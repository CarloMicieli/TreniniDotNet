using System;

namespace TreniniDotNet.Application.Boundaries.Collection.CreateCollection
{
    public sealed class CreateCollectionOutput : IUseCaseOutput
    {
        public CreateCollectionOutput(Guid id, string owner)
        {
            Id = id;
            Owner = owner;
        }

        public Guid Id { get; }
        public string Owner { get; }
    }
}
