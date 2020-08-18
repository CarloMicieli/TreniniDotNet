using System;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
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
