using MediatR;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    public sealed class CreateCollectionRequest : IRequest
    {
        public string? Owner { get; set; }

        public string? Notes { get; set; }
    }
}
