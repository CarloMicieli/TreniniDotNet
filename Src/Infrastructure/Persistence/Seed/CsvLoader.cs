using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace TreniniDotNet.Infrastructure.Persistence.Seed
{
    public sealed class CsvLoader
    {
        public static IEnumerable<TRecord> LoadRecords<TRecord>(string filename)
        {
            using var reader = new StreamReader(filename);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<TRecord>().ToList();
        }
    }
}
