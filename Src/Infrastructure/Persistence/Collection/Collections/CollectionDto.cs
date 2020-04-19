﻿using System;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Collections
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal class CollectionDto
    {
        public Guid collection_id { get; set; }
        public string owner { get; set; } = null!;
        public DateTime created { get; set; }
        public DateTime? last_modified { get; set; }
        public int version { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
