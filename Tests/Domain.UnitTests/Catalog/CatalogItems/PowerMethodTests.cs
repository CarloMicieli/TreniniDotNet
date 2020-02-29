using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class PowerMethodTests
    {
        [Fact]
        public void PowerMethod_shouldBeConvertedFromStrings()
        {
            Assert.Equal(
                PowerMethod.AC,
                PowerMethod.AC.ToString().ToPowerMethod());
        }

        [Fact]
        public void PowerMethod_shouldBeFailToConvertedInvalidStrings()
        {
            Assert.Null("not-valid".ToPowerMethod());
        }   
    }
}