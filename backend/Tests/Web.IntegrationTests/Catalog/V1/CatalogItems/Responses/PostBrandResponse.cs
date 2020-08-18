namespace TreniniDotNet.IntegrationTests.Catalog.V1.CatalogItems.Responses
{
    public class PostCatalogItemResponse
    {
        public SlugResponse Slug { set; get; }
    }

    public class SlugResponse
    {
        public string Value { set; get; }
    }
}
