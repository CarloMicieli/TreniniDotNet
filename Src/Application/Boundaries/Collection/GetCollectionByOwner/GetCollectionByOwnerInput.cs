using System;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerInput : IUseCaseInput
    {
        public GetCollectionByOwnerInput(Guid id, string owner)
        {
            Id = id;
            Owner = owner;
        }

        public Guid Id { get; }
        public string Owner { get; }
    }
}
