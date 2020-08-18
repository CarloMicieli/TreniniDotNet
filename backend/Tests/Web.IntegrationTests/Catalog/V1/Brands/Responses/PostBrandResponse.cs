namespace TreniniDotNet.IntegrationTests.Catalog.V1.Brands.Responses
{
    public class PostBrandResponse
    {
        public SlugResponse Slug { set; get; }
    }

    public class SlugResponse
    {
        public string Value { set; get; }
    }
}
