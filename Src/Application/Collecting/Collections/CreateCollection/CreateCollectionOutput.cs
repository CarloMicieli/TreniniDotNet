using System;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
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
