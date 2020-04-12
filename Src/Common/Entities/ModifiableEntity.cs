using NodaTime;
using System;

namespace TreniniDotNet.Common.Entities
{
    public abstract class ModifiableEntity : IModifiableEntity
    {
        public Instant CreatedDate { get; }

        public Instant? ModifiedDate { get; }

        public int Version { get; }

        protected ModifiableEntity(Instant created, Instant? modified, int version)
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
