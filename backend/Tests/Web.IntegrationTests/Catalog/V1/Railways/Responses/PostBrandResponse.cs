namespace TreniniDotNet.IntegrationTests.Catalog.V1.Railways.Responses
{
    public class PostRailwayResponse
    {
        public SlugResponse Slug { set; get; }
    }

    public class SlugResponse
    {
        public string Value { set; get; }
    }
}
