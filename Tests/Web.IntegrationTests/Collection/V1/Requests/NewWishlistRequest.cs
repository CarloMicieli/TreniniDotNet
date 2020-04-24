namespace TreniniDotNet.Web.IntegrationTests.Collection.V1.Requests
{
    public class NewWishlistRequest
    {
        public string Owner { set; get; }

        public string ListName { set; get; }

        public string Visibility { set; get; }
    }
}