using System;
using System.Collections.Generic;

namespace DataSeeding.DataLoader.Records
{
    public abstract class DataSet<TElement>
    {
        public string Description { set; get; }
        public int Version { set; get; }
        public DateTime ModifiedAt { set; get; }
        public IEnumerable<TElement> Elements { set; get; }
    }
}
