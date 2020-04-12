using Xunit;
using FluentAssertions;
using System;
using System.Collections.Generic;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public class ScaleStandardTests
    {
        [Fact]
        public void ScaleStandard_TryParse_ShouldParseValidStrings()
        {
            var result = ScaleStandards.TryParse(ScaleStandard.NEM.ToString(), out var standard);

            result.Should().BeTrue();
            standard.Should().Be(ScaleStandard.NEM);
        }

        [Fact]
        public void ScaleStandard_TryParse_ShouldReturnNullValueForInvalidStrings()
        {
            var result = ScaleStandards.TryParse("invalid", out var standard);

            result.Should().BeFalse();
            standard.Should().BeNull();
        }

        [Fact]
        public void ScaleStandard_Parse_ShouldParseValidStrings()
        {
            var standard = ScaleStandards.Parse(ScaleStandard.NEM.ToString());
            standard.Should().Be(ScaleStandard.NEM);
        }

        [Fact]
        public void ScaleStandard_Parse_ShouldThrowExceptionForInvalidStrings()
        {
            Action act = () => ScaleStandards.Parse("invalid");
            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void ScaleStandard_ToSet_ShouldConvertListOfStrings()
        {
            IEnumerable<string> values = new List<string>
            {
                ScaleStandard.NEM.ToString(),
                ScaleStandard.NRMA.ToString()
            };

            var set = ScaleStandards.ToSet(values);

            set.Should().NotBeNull();
            set.Should().HaveCount(2);
            set.Should().Contain(ScaleStandard.NEM);
            set.Should().Contain(ScaleStandard.NRMA);
        }

        [Fact]
        public void ScaleStandard_ToSet_ShouldReturnEmptySetForNull()
        {
            var set = ScaleStandards.ToSet(null);

            set.Should().NotBeNull();
            set.Should().HaveCount(0);
        }
    }
}