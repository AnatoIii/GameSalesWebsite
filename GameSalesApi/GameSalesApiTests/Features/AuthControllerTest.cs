using DataAccess;
using FluentAssertions;
using GameSalesApi;
using GameSalesApi.Features.Authorization;
using GameSalesApi.Features.Authorization.Commands;
using GameSalesApi.Helpers;
using GameSalesApiTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TestsInfrastructure;
using Xunit;

namespace GameSalesApiTests.Features
{
    public class AuthControllerTest
    {
        #region Test classes

        private readonly TestLogger<AuthController> _rTestLogger;
        private readonly GameSalesContext _rDbContext;
        private readonly List<User> _testData;
        private readonly TokenCreator _rTokenCreator;

        #endregion

        public AuthControllerTest()
        {
            _rTestLogger = new TestLogger<AuthController>();

            _testData = _GetTestableUserData();

            _rDbContext = DbHelper.GetTestContextWithTargetParams("User Test", _testData);

            _rTokenCreator = new TokenCreator(Options.Create(new TokenConfig()
            {
                JWTLifetime = 60,
                RefreshTokenLifetime = 43200,
                Secret = "TestSecret1234",
                Audience = "GameSales"
            }));
        }

        #region LoginTests

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AuthController")]
        [Trait("Category", "Login")]
        public void AuthController_Login_User_Not_Found_Works_Fine()
        {
            AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

            string testEmail = "incorectEmail";
            LoginCommand loginCommand = new LoginCommand()
            {
                Email = testEmail
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = authController.Login(loginCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(loginCommand)} failed. Message: No such user, Email: {testEmail}");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AuthController")]
        [Trait("Category", "Login")]
        public void AuthController_Login_Incorrect_Password_Works_Fine()
        {
            AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

            string testEmail = _testData[0].Email;
            string testPass = "test pass";
            LoginCommand loginCommand = new LoginCommand()
            {
                Email = testEmail,
                Password = testPass
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = authController.Login(loginCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(loginCommand)} failed. Message: Incorrect password");
        }

        //// error due to incorrect setup for TokenCreator...
        //[Fact]
        //public void AuthController_Login_Works_Fine()
        //{
        //    AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

        //    string testEmail = _testData[0].Email;
        //    string testPass = "Testpass1";
        //    LoginCommand loginCommand = new LoginCommand()
        //    {
        //        Email = testEmail,
        //        Password = testPass
        //    };

        //    _rTestLogger.LoggedMessages.Should().BeEmpty();

        //    var response = authController.Login(loginCommand);

        //    _rTestLogger.LoggedMessages.Should().NotBeEmpty()
        //        .And.HaveCount(1);

        //    var okObjectResult = response as OkObjectResult;
        //    okObjectResult.Should().NotBeNull();

        //    okObjectResult.StatusCode.Should().NotBeNull()
        //        .And.Be(200);

        //    string result = okObjectResult.Value.ToString();

        //    result.Should().NotBeNull()
        //        .And.Be($"{nameof(loginCommand)} failed. Message: Incorrect password");
        //}

        #endregion

        #region CreateNewUserTests

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AuthController")]
        [Trait("Category", "CreateNewUser")]
        public void AuthController_CreateNewUser_User_With_Same_Email_Exist_Works_Fine()
        {
            AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

            string testEmail = _testData[0].Email;
            string testPass = "test pass";
            NewUserCommand newUserCommand = new NewUserCommand()
            {
                Email = testEmail,
                Password = testPass
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = authController.CreateNewUser(newUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(newUserCommand)} failed. Message: User with email `{testEmail}` already exist!");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AuthController")]
        [Trait("Category", "CreateNewUser")]
        public void AuthController_CreateNewUser_Incorrect_Password_Works_Fine()
        {
            AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

            string testEmail = "testEmail";
            string testPass = "test pass";
            NewUserCommand newUserCommand = new NewUserCommand()
            {
                Email = testEmail,
                Password = testPass
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = authController.CreateNewUser(newUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(newUserCommand)} failed. Message: Password should contain at least one upper case letter.");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AuthController")]
        [Trait("Category", "CreateNewUser")]
        public void AuthController_CreateNewUser_Works_Fine()
        {
            AuthController authController = new AuthController(_rDbContext, _rTestLogger, _rTokenCreator);

            string testEmail = "testEmail";
            string testPass = "testCorrectPass123";
            NewUserCommand newUserCommand = new NewUserCommand()
            {
                Email = testEmail,
                Password = testPass
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();
            _rDbContext.Users.Should().HaveCount(2);

            var response = authController.CreateNewUser(newUserCommand);

            _rDbContext.Users.Should().HaveCount(3);
            _rDbContext.Users.Where(u => u.Email == testEmail).FirstOrDefault().Should().NotBeNull();

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var okResult = response as OkResult;
            okResult.Should().NotBeNull();

            okResult.StatusCode.Should().Be(200);
        }

        #endregion

        #region RefreshTokenTests

        #endregion

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

            passwordSalt = PasswordHelpers.GenerateSalt();
            passwordHash = PasswordHelpers.HashPassword("Testpass2", passwordSalt);

            data.Add(new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "FNTest2",
                LastName = "LNTest2",
                Email = "TestEmail2",
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
