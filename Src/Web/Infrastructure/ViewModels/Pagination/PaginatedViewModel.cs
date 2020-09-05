using System.Collections.Generic;
using System.Text.Json.Serialization;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Web.Infrastructure.ViewModels.Links;

namespace TreniniDotNet.Web.Infrastructure.ViewModels.Pagination
{
    public sealed class PaginatedViewModel<TViewModel>
    {
        [JsonPropertyName("_links")]
        public PaginateLinksView Links { set; get; } = null!;

        public int Limit { set; get; } = Page.Default.Limit;

        public List<TViewModel> Results { set; get; } = new List<TViewModel>();
    }
}
