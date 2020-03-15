using Xunit;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public class EraTests
    {
        [Fact]
        public void Era_shouldBeConvertedFromStrings()
        {
            Assert.Equal(
                Era.III,
                Era.III.ToString().ToEra()
            );
        }

        [Fact]
        public void Era_shouldBeFailToConvertedInvalidStrings()
        {
            Assert.Null("not-valid".ToEra());
        }
    }
}