using DataAccess;
using FluentAssertions;
using GameSalesApi;
using GameSalesApi.Features.Authorization;
using GameSalesApi.Helpers;
using GameSalesApiTests.Helpers;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TestsInfrastructure;
using Xunit;

namespace GameSalesApiTests.Features
{
    public class TokenCreatorTest
    {
        #region Test classes

        private readonly GameSalesContext _rDbContext;
        private readonly List<User> _testData;
        private readonly TokenCreator _rTokenCreator;

        #endregion

        public TokenCreatorTest()
        {
            _testData = _GetTestableUserData();

            _rDbContext = DbHelper.GetTestContextWithTargetParams("User Test", _testData);

            _rTokenCreator = new TokenCreator(Options.Create(new TokenConfig()
            {
                JWTLifetime = 60,
                RefreshTokenLifetime = 43200,
                Secret = "TestSecretSuperDuperSecret",
                Audience = "GameSales",
            }));
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Authorization")]
        [Trait("Category", "Timeout")]
        public void CreateDTOToken_Works_Fine()
        {
            User testUser = _testData.First();

            var token = _rTokenCreator.CreateDTOToken(testUser, _rDbContext);

            token.Should().NotBeNull();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Authorization")]
        [Trait("Category", "Timeout")]
        public void CreateDTOToken_10Times_Works_Fine_ForAssumptedTime()
        {
            User testUser = _testData.First();

            CodeExecutionMetric.ActionChecker(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    var token = _rTokenCreator.CreateDTOToken(testUser, _rDbContext);

                    token.Should().NotBeNull();
                }
            }).Seconds.Should().BeLessThan(10);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Authorization")]
        [Trait("Category", "Timeout")]
        public void CreateDTOToken_Fails_ForLowTime()
        {
            User testUser = _testData.First();

            Action act = () => CodeExecutionMetric.ActionChecker(() =>
                {
                    var token = _rTokenCreator.CreateDTOToken(testUser, _rDbContext);

                    token.Should().NotBeNull();
                }, 10);

            act.Should().Throw<ArgumentException>();
        }

        private List<User> _GetTestableUserData()
        {
            var data = new List<User>();

            byte[] passwordSalt = PasswordHelpers.GenerateSalt();
            string passwordHash = PasswordHelpers.HashPassword("Testpass1", passwordSalt);

            data.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "FNTest1",
                LastName = "LNTest1",
                Email = "TestEmail1",
                Role = Model.Enums.UserRole.Default,
                NotificationViaEmail = false,
                NotificationViaTelegram = false,
                PhotoLink = null,
                PasswordHash = passwordHash,
                PasswordSalt = Convert.ToBase64String(passwordSalt)
            });

            return data;
        }
    }
}
