using DataAccess;
using FluentAssertions;
using GameSalesApi.Features.AccountManagement;
using GameSalesApi.Features.AccountManagement.Commands;
using GameSalesApi.Features.AccountManagement.Queries;
using GameSalesApi.Helpers;
using GameSalesApiTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using TestsInfrastructure;
using Xunit;

namespace GameSalesApiTests.Features
{
    public class AccountManagementTest
    {
        #region Test classes

        private readonly TestLogger<AccountController> _rTestLogger;
        private readonly GameSalesContext _rDbContext;
        private readonly List<User> _testData;

        #endregion

        public AccountManagementTest()
        {
            _rTestLogger = new TestLogger<AccountController>();

            _testData = _GetTestableUserData();

            _rDbContext = DbHelper.GetTestContextWithTargetParams("User Test", _testData);
        }

        #region GetAllTests

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "GetAll")]
        public void AccountController_GetAll_With_Some_Data_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.GetAll();

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            okObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(200);

            List<User> result = okObjectResult.Value as List<User>;

            result.Should().NotBeNull()
                .And.HaveCount(2)
                .And.BeEquivalentTo(_testData);
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "GetUser")]
        public void AccountController_GetUser_That_Exists_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            GetUserQuery getUserQuery = new GetUserQuery()
            {
                UserId = _testData[0].Id
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.GetUser(getUserQuery);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var okObjectResult = response as OkObjectResult;
            okObjectResult.Should().NotBeNull();

            okObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(200);

            User result = okObjectResult.Value as User;

            result.Should().NotBeNull()
                .And.BeEquivalentTo(_testData[0]);
        }

        #endregion

        #region GetUserTests

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "GetUser")]
        public void AccountController_GetUser_Empty_Guid_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            GetUserQuery getUserQuery = new GetUserQuery()
            {
                UserId = Guid.Empty
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.GetUser(getUserQuery);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(getUserQuery)} failed. Message: User not found. User can`t be with empty id.");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "GetUser")]
        public void AccountController_GetUser_User_Not_Found_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            Guid testId = Guid.NewGuid();
            GetUserQuery getUserQuery = new GetUserQuery()
            {
                UserId = testId
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.GetUser(getUserQuery);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(getUserQuery)} failed. Message: User not found. User with id {testId} not found.");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "GetUser")]
        public void AccountController_GetUser_User_Id_Cant_Be_Empty_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            GetUserQuery getUserQuery = new GetUserQuery()
            {
                UserId = Guid.Empty
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.GetUser(getUserQuery);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(getUserQuery)} failed. Message: User not found. User can`t be with empty id.");
        }

        #endregion

        #region UpdateUser

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "UpdateUser")]
        public void AccountController_UpdateUser_User_Not_Found_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            Guid testId = Guid.NewGuid();
            UpdateUserCommand updateUserCommand = new UpdateUserCommand()
            {
                UserId = testId
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.UpdateUser(updateUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(updateUserCommand)} failed. Message: User with id {testId} not found!");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "UpdateUser")]
        public void AccountController_UpdateUser_Incorrect_Password_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            UpdateUserCommand updateUserCommand = new UpdateUserCommand()
            {
                UserId = _testData[0].Id,
                Password = "ii"
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.UpdateUser(updateUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(updateUserCommand)} failed. Message: Password should contain at least one upper case letter.");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "UpdateUser")]
        public void AccountController_UpdateUser_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            string testFirstName = "New First Name";
            UpdateUserCommand updateUserCommand = new UpdateUserCommand()
            {
                UserId = _testData[0].Id,
                FirstName = testFirstName
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.UpdateUser(updateUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var okObjectResult = response as OkResult;
            okObjectResult.Should().NotBeNull();

            okObjectResult.StatusCode.Should().Be(200);

            User updatedUser = _rDbContext.Users.SingleOrDefault(u => u.Id == _testData[0].Id);

            updatedUser.Should().NotBeNull();
            updatedUser.Should().BeEquivalentTo(_testData[0], options =>
                options.Excluding(o => o.FirstName));
            updatedUser.FirstName.Should().Be(testFirstName);
        }

        #endregion

        #region RemoveUser

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "RemoveUser")]
        public void AccountController_RemoveUser_User_Not_Found_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            Guid testId = Guid.NewGuid();
            RemoveUserCommand removeUserCommand = new RemoveUserCommand()
            {
                UserId = testId
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();

            var response = accountController.RemoveUser(removeUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var badRequestObjectResult = response as BadRequestObjectResult;
            badRequestObjectResult.Should().NotBeNull();

            badRequestObjectResult.StatusCode.Should().NotBeNull()
                .And.Be(400);

            string result = badRequestObjectResult.Value.ToString();

            result.Should().NotBeNull()
                .And.Be($"{nameof(removeUserCommand)} failed. Message: User with id {testId} not found!");
        }

        [Fact]
        [Trait("Category", "IntegrationTests")]
        [Trait("Category", "AccountController")]
        [Trait("Category", "RemoveUser")]
        public void AccountController_RemoveUser_Works_Fine()
        {
            AccountController accountController = new AccountController(_rDbContext, _rTestLogger);

            Guid testGuid = _testData[0].Id;
            RemoveUserCommand removeUserCommand = new RemoveUserCommand()
            {
                UserId = testGuid
            };

            _rTestLogger.LoggedMessages.Should().BeEmpty();
            _rDbContext.Users.Should().HaveCount(2);

            var response = accountController.RemoveUser(removeUserCommand);

            _rTestLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            var okObjectResult = response as OkResult;
            okObjectResult.Should().NotBeNull();

            okObjectResult.StatusCode.Should().Be(200);

            User removedUser = _rDbContext.Users.SingleOrDefault(u => u.Id == _testData[0].Id);

            removedUser.Should().BeNull();
            _rDbContext.Users.Should().HaveCount(1);
        }

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
