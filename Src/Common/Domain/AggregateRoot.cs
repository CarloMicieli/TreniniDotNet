using System;
using NodaTime;

namespace TreniniDotNet.Common.Domain
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>
        where TKey : struct, IEquatable<TKey>
    {
        public Instant CreatedDate { get; }

        public Instant? ModifiedDate { get; }

        public int Version { get; }

        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TKey id, Instant created, Instant? modified, int version)
            : base(id)
        {
            if (version < 0)
            {
                throw new ArgumentException("Version must be positive", nameof(version));
            }

            CreatedDate = created;
            ModifiedDate = modified;
            Version = version;
        }
    }
}
