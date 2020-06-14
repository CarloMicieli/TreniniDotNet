using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Enums
{
    public class EnumHelpersTests
    {
        [Fact]
        public void EnumHelpers_ShouldCreateRequiredValue_FromValidStrings()
        {
            var expected = TestEnum.FirstConstant;
            var result = EnumHelpers.RequiredValueFor<TestEnum>(expected.ToString());
            result.Should().Be(expected);
        }

        [Fact]
        public void EnumHelpers_ShouldCreateRequiredValue_FromValidStringsRegardlessTheCase()
        {
            var expected = TestEnum.FirstConstant;

            var result1 = EnumHelpers.RequiredValueFor<TestEnum>(expected.ToString().ToLower());
            var result2 = EnumHelpers.RequiredValueFor<TestEnum>(expected.ToString().ToUpper());
            result1.Should().Be(expected);
            result2.Should().Be(expected);
        }

        [Fact]
        public void EnumHelpers_RequiredValueFor_ShouldThrowExceptionFromInvalidStrings()
        {
            Action act = () => EnumHelpers.RequiredValueFor<TestEnum>("invalid");
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("The value 'invalid' was not a valid constant for TestEnum.");
        }

        [Fact]
        public void EnumHelpers_ShouldCreateOptionalValue_FromValidStrings()
        {
            var expected = TestEnum.FirstConstant;

            var result = EnumHelpers.OptionalValueFor<TestEnum>(expected.ToString().ToLower());
            result.Should().NotBeNull();
            result.Should().Be(expected);
        }

        [Fact]
        public void EnumHelpers_ShouldCreateOptionalValue_FromValidStringsRegardlessTheCase()
        {
            var expected = TestEnum.FirstConstant;

            var result1 = EnumHelpers.OptionalValueFor<TestEnum>(expected.ToString().ToLower());
            var result2 = EnumHelpers.OptionalValueFor<TestEnum>(expected.ToString().ToUpper());

            result1.Should().NotBeNull();
            result1.Should().Be(expected);
            result2.Should().NotBeNull();
            result2.Should().Be(expected);
        }

        [Fact]
        public void EnumHelpers_ShouldReturnNullForOptionalValue_WhenStringIsInvalid()
        {
            var result = EnumHelpers.OptionalValueFor<TestEnum>("invalid");
            result.Should().BeNull();
        }
    }

    public enum TestEnum
    {
        FirstConstant,
        SecondConstant
    }
}
