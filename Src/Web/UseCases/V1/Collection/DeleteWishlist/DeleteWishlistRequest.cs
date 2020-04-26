using MediatR;
using Newtonsoft.Json;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistRequest : IRequest
    {
        [JsonIgnore]
        public string? Owner { get; set; }
        public Guid Id { get; set; }
    }
}
