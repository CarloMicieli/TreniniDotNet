using System.Collections.Generic;
using System.Text.Json.Serialization;
using TreniniDotNet.Domain.Pagination;
using TreniniDotNet.Web.ViewModels.Links;

namespace TreniniDotNet.Web.ViewModels.Pagination
{
    public sealed class PaginatedViewModel<TViewModel>
    {
        [JsonPropertyName("_links")]
        public PaginateLinksView Links { set; get; } = null!;

        public int Limit { set; get; } = Page.Default.Limit;

        public List<TViewModel> Results { set; get; } = new List<TViewModel>();
    }
}
