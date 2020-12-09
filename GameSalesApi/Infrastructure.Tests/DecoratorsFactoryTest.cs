using Xunit;
using Infrastructure.DecoratorsFactory;
using FluentAssertions;
using Infrastructure.CommandBase;
using Infrastructure.Result;
using Infrastructure.HandlerBase;
using Microsoft.EntityFrameworkCore;
using Moq;
using TestsInfrastructure;

namespace Infrastructure.Tests
{
    public class DecoratorsFactoryTest
    {
        #region TestData

        private static readonly string _rTestOKCommand = "test OK command";
        private static readonly string _rTestFailCommand = "test FAIL command";
        private static readonly string _rTestOKQuery = "test OK query";
        private static readonly string _rTestFailQuery = "test FAIL query";

        public static class DecoratorWorkHandler
        {
            public static bool IsProceseed { get; set; }
        }

        public class TestCommand : ICommand<Result<string>>
        {
            public string Name { get; set; }
        }

        public class TestQuery : IQuery<Result<string>>
        {
            public string Name { get; set; }
        }

        public class TestOkCommandHandler : CommandHandlerDecoratorBase<TestCommand, Result<string>>
        {
            public TestOkCommandHandler()
                : base(null)
            { }

            public override void Execute(TestCommand command)
                => Handle(command);

            public override Result<string> Handle(TestCommand input)
            {
                DecoratorWorkHandler.IsProceseed = true;

                return Result.Result.Ok(_rTestOKCommand + input.Name);
            }
        }

        public class TestFailCommandHandler : CommandHandlerDecoratorBase<TestCommand, Result<string>>
        {
            public TestFailCommandHandler()
                : base(null)
            { }

            public override void Execute(TestCommand command)
                => Handle(command);

            public override Result<string> Handle(TestCommand input)
            {
                DecoratorWorkHandler.IsProceseed = true;

                return Result.Result.Fail<string>(_rTestFailCommand + input.Name);
            }
        }

        public class TestOkQuryHandler : QueryHandlerDecoratorBase<TestQuery, Result<string>>
        {
            public TestOkQuryHandler()
                : base(null)
            { }

            public override Result<string> Handle(TestQuery input)
            {
                DecoratorWorkHandler.IsProceseed = true;

                return Result.Result.Ok(_rTestOKQuery + input.Name);
            }
        }

        public class TestFailQuryHandler : QueryHandlerDecoratorBase<TestQuery, Result<string>>
        {
            public TestFailQuryHandler()
                : base(null)
            { }

            public override Result<string> Handle(TestQuery input)
            {
                DecoratorWorkHandler.IsProceseed = true;

                return Result.Result.Fail<string>(_rTestFailQuery + input.Name);
            }
        }

        #endregion

        public DecoratorsFactoryTest()
        {
            DecoratorWorkHandler.IsProceseed = false;
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "CommandDecoratorBuilder")]
        public void Command_Handler_For_Result_Ok_Works_As_Expected()
        {
            var handler = new CommandDecoratorBuilder<TestCommand, Result<string>>()
                .Add<TestOkCommandHandler>()
                .Build();

            TestCommand testCommand = new TestCommand() { Name = "Test" };

            testCommand.Should().NotBeNull();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testCommand);

            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Error.Should().Be(string.Empty);
            result.Value.Should().Be(_rTestOKCommand + testCommand.Name);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "CommandDecoratorBuilder")]
        public void Command_Handler_For_Result_Fail_Works_As_Expected()
        {
            var handler = new CommandDecoratorBuilder<TestCommand, Result<string>>()
                .Add<TestFailCommandHandler>()
                .Build();

            TestCommand testCommand = new TestCommand() { Name = "Test" };

            testCommand.Should().NotBeNull();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testCommand);

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Error.Should().Be(_rTestFailCommand + testCommand.Name);
            result.Value.Should().Be(default);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "CommandDecoratorBuilder")]
        public void Command_Handler_With_Base_Decorators_Works_As_Expected()
        {
            var testLogger = new TestLogger<DecoratorsFactoryTest>();
            var testContext = new Mock<DbContext>();

            var handler = new CommandDecoratorBuilder<TestCommand, Result<string>>()
                .Add<TestOkCommandHandler>()
                .AddBaseDecorators(testLogger, testContext.Object)
                .Build();

            TestCommand testCommand = new TestCommand() { Name = "Test" };

            testCommand.Should().NotBeNull();
            testLogger.LoggedMessages.Should().BeEmpty();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testCommand);

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Error.Should().Be(_rTestFailCommand + testCommand.Name);
            result.Value.Should().Be(default);
            testLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "QueryDecoratorBuilder")]
        public void Query_Handler_For_Result_Ok_Works_As_Expected()
        {
            var handler = new QueryDecoratorBuilder<TestQuery, Result<string>>()
                .Add<TestOkQuryHandler>()
                .Build();

            TestQuery testQuery = new TestQuery() { Name = "Test" };

            testQuery.Should().NotBeNull();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testQuery);

            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Error.Should().Be(string.Empty);
            result.Value.Should().Be(_rTestOKQuery + testQuery.Name);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "QueryDecoratorBuilder")]
        public void Query_Handler_For_Result_Fail_Works_As_Expected()
        {
            var handler = new QueryDecoratorBuilder<TestQuery, Result<string>>()
                .Add<TestFailQuryHandler>()
                .Build();

            TestQuery testQuery = new TestQuery() { Name = "Test" };

            testQuery.Should().NotBeNull();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testQuery);

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Error.Should().Be(_rTestFailQuery + testQuery.Name);
            result.Value.Should().Be(default);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "QueryDecoratorBuilder")]
        public void Query_Handler_With_Base_Decorators_Works_As_Expected()
        {
            var testLogger = new TestLogger<DecoratorsFactoryTest>();

            var handler = new QueryDecoratorBuilder<TestQuery, Result<string>>()
                .Add<TestOkQuryHandler>()
                .AddBaseDecorators(testLogger)
                .Build();

            TestQuery testQuery = new TestQuery() { Name = "Test" };

            testQuery.Should().NotBeNull();
            testLogger.LoggedMessages.Should().BeEmpty();

            DecoratorWorkHandler.IsProceseed.Should().BeFalse();

            var result = handler.Handle(testQuery);

            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Error.Should().Be(string.Empty);
            result.Value.Should().Be(_rTestOKQuery + testQuery.Name);
            testLogger.LoggedMessages.Should().NotBeEmpty()
                .And.HaveCount(1);

            DecoratorWorkHandler.IsProceseed.Should().BeTrue();
        }
    }
}
