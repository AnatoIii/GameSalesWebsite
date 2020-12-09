using Xunit;
using FluentAssertions;
using Infrastructure.Results;

namespace Infrastructure.Tests
{
    public class ResultTest
    {
        private readonly string _rErrorMessage = "test error";
        private readonly string _rOkMessage = "test ok";
        private readonly int _rTestIntValue = 5;

        public ResultTest()
        { }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Simple_Result_Ok_Works_As_Expected()
        {
            Result result = Result.Ok();

            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Error.Should().Be(string.Empty);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Simple_Result_Fail_Works_As_Expected()
        {
            Result result = Result.Fail(_rErrorMessage);

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Error.Should().Be(_rErrorMessage);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Generic_Result_Ok_Works_As_Expected()
        {
            Result<int> result = Result.Ok(_rTestIntValue);

            result.Success.Should().BeTrue();
            result.Failure.Should().BeFalse();
            result.Error.Should().Be(string.Empty);

            result.Value.Should().Be(_rTestIntValue);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Generic_Result_Fail_Works_As_Expected()
        {
            Result<int> result = Result.Fail<int>(_rErrorMessage);

            result.Success.Should().BeFalse();
            result.Failure.Should().BeTrue();
            result.Error.Should().Be(_rErrorMessage);

            result.Value.Should().Be(default);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Simple_Result_Combine_Works_As_Expected()
        {
            Result result1 = Result.Ok();
            Result result2 = Result.Ok();
            Result result3 = Result.Ok();

            result1.Success.Should().BeTrue();
            result1.Failure.Should().BeFalse();
            result1.Error.Should().Be(string.Empty);

            result2.Success.Should().BeTrue();
            result2.Failure.Should().BeFalse();
            result2.Error.Should().Be(string.Empty);

            result3.Success.Should().BeTrue();
            result3.Failure.Should().BeFalse();
            result3.Error.Should().Be(string.Empty);

            Result[] results = new Result[3] { result1, result2, result3 };

            results.Should().HaveCount(3);

            var res = Result.Combine(results);

            res.Success.Should().BeTrue();
            res.Failure.Should().BeFalse();
            res.Error.Should().Be(string.Empty);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Simple_Result_Combine_Works_As_Expected_For_Fail()
        {
            Result result1 = Result.Ok();
            Result result2 = Result.Fail(_rErrorMessage);
            Result result3 = Result.Ok();

            result1.Success.Should().BeTrue();
            result1.Failure.Should().BeFalse();
            result1.Error.Should().Be(string.Empty);

            result2.Success.Should().BeFalse();
            result2.Failure.Should().BeTrue();
            result2.Error.Should().Be(_rErrorMessage);

            result3.Success.Should().BeTrue();
            result3.Failure.Should().BeFalse();
            result3.Error.Should().Be(string.Empty);

            Result[] results = new Result[3] { result1, result2, result3 };

            results.Should().HaveCount(3);

            var res = Result.Combine(results);

            res.Success.Should().BeFalse();
            res.Failure.Should().BeTrue();
            res.Error.Should().Be(_rErrorMessage);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Generic_Result_Combine_Works_As_Expected()
        {
            Result<int> result1 = Result.Ok(_rTestIntValue);
            Result<int> result2 = Result.Ok(_rTestIntValue);
            Result<string> result3 = Result.Ok(_rOkMessage);

            result1.Success.Should().BeTrue();
            result1.Failure.Should().BeFalse();
            result1.Error.Should().Be(string.Empty);
            result1.Value.Should().Be(_rTestIntValue);

            result2.Success.Should().BeTrue();
            result2.Failure.Should().BeFalse();
            result2.Error.Should().Be(string.Empty);
            result2.Value.Should().Be(_rTestIntValue);

            result3.Success.Should().BeTrue();
            result3.Failure.Should().BeFalse();
            result3.Error.Should().Be(string.Empty);
            result3.Value.Should().Be(_rOkMessage);

            Result[] results = new Result[3] { result1, result2, result3 };

            results.Should().HaveCount(3);

            var res = Result.Combine(results);

            res.Success.Should().BeTrue();
            res.Failure.Should().BeFalse();
            res.Error.Should().Be(string.Empty);
        }

        [Fact]
        [Trait("Category", "UnitTests")]
        [Trait("Category", "Result")]
        public void Generic_Result_Combine_Works_As_Expected_For_Fail()
        {
            Result<int> result1 = Result.Ok(_rTestIntValue);
            Result<int> result2 = Result.Fail<int>(_rErrorMessage);
            Result<string> result3 = Result.Ok(_rOkMessage);

            result1.Success.Should().BeTrue();
            result1.Failure.Should().BeFalse();
            result1.Error.Should().Be(string.Empty);
            result1.Value.Should().Be(_rTestIntValue);

            result2.Success.Should().BeFalse();
            result2.Failure.Should().BeTrue();
            result2.Error.Should().Be(_rErrorMessage);
            result2.Value.Should().Be(default);

            result3.Success.Should().BeTrue();
            result3.Failure.Should().BeFalse();
            result3.Error.Should().Be(string.Empty);
            result3.Value.Should().Be(_rOkMessage);

            Result[] results = new Result[3] { result1, result2, result3 };

            results.Should().HaveCount(3);

            var res = Result.Combine(results);

            res.Success.Should().BeFalse();
            res.Failure.Should().BeTrue();
            res.Error.Should().Be(_rErrorMessage);
        }
    }
}
