using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerRequest : IRequest
    {
        public string? Owner { get; set; }
    }
}
