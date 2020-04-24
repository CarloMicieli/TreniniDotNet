using MediatR;
using System;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerRequest : IRequest
    {
        public Guid Id { get; set; }
        public string? Owner { get; set; }
    }
}
