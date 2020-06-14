using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class ServiceLevelTests
    {
        [Fact]
        public void ServiceLevel_Parse_ShouldParseValidValues()
        {
            var firstClass = ServiceLevel.Parse("1cl");
            firstClass.Should().Be(ServiceLevel.FirstClass);
        }

        [Fact]
        public void ServiceLevel_Parse_ShouldFailToParseInvalidValues()
        {
            Action act = () => ServiceLevel.Parse("---");
            act.Should().Throw<ArgumentOutOfRangeException>()
                .WithMessage("The value is not a valid Service Level [Allowed values are: 1cl, 2cl, 3cl] (Parameter 'str')");
        }

        [Fact]
        public void ServiceLevel_TryParse_ShouldParseValidValues()
        {
            var result = ServiceLevel.TryParse("1cl", out var firstClass);

            result.Should().BeTrue();
            firstClass.Should().Be(ServiceLevel.FirstClass);
        }

        [Fact]
        public void ServiceLevel_TryParse_ShouldFailToParseInvalidValues()
        {
            var result = ServiceLevel.TryParse("---", out var _);

            result.Should().BeFalse();
        }

        [Fact]
        public void ServiceLevel_TryParse_ShouldParseMixedServiceLevels()
        {
            var result = ServiceLevel.TryParse("2cl/3cl/1cl", out var serviceLevel);
            result.Should().BeTrue();
            serviceLevel?.ToString().Should().Be("1cl/2cl/3cl");
        }

        [Fact]
        public void ServiceLevel_ToServiceLevelOpt_ShouldProduceValueFromStrings()
        {
            var serviceLevel1 = ((string)null).ToServiceLevelOpt();
            var serviceLevel2 = "1cl".ToServiceLevelOpt();

            serviceLevel1.Should().BeNull();
            serviceLevel2.Should().NotBeNull();
            serviceLevel2.Should().Be(ServiceLevel.FirstClass);
        }

        [Fact]
        public void ServiceLevel_ShouldDefineValueForTheServiceLevels()
        {
            ServiceLevel.FirstClass.ToString().Should().Be("1cl");
            ServiceLevel.FirstSecondClass.ToString().Should().Be("1cl/2cl");
            ServiceLevel.SecondClass.ToString().Should().Be("2cl");
            ServiceLevel.SecondThirdClass.ToString().Should().Be("2cl/3cl");
            ServiceLevel.ThirdClass.ToString().Should().Be("3cl");
        }
    }
}
