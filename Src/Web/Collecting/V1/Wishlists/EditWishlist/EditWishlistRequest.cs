using System;
using MediatR;
using Newtonsoft.Json;
using TreniniDotNet.Web.Collecting.V1.Wishlists.Common.Requests;

namespace TreniniDotNet.Web.Collecting.V1.Wishlists.EditWishlist
{
    public sealed class EditWishlistRequest : IRequest
    {
        [JsonIgnore]
        public Guid Id { set; get; }
        [JsonIgnore]
        public string? Owner { set; get; }

        public string? ListName { set; get; }

        public string Visibility { set; get; }

        public BudgetRequest? Budget { set; get; }
    }
}
