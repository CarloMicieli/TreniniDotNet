using System;

namespace TreniniDotNet.Common.Domain
{
    public abstract class AggregateRootRef<TAggregate, TKey>
        where TKey: struct, IEquatable<TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        protected AggregateRootRef(TKey id, string slug, string stringRepresentation)
        {
            Id = id;
            Slug = slug;
            StringRepresentation = stringRepresentation;
        }

        public TKey Id { get; }
        public string Slug { get; }
        private string StringRepresentation { get; }

        public override string ToString() => StringRepresentation;
    }
}