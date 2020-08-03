using System;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Infrastructure.Persistence.Repositories.Collecting.Collections
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CollectionParam
    {
        public Guid collection_id { get; set; }
        public string owner{ get; set; }
        public string? notes { set; get; }
        public DateTime created { set; get; }
        public DateTime? last_modified { set; get; }
        public int version { set; get; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}