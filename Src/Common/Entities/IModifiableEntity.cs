using NodaTime;

namespace TreniniDotNet.Common.Entities
{
    public interface IModifiableEntity
    {
        Instant CreatedDate { get; }

        Instant? ModifiedDate { get; }

        int Version { get; }
    }
}
