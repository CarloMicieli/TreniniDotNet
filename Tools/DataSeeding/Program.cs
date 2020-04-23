using System.Drawing;
using Console = Colorful.Console;
using DataSeeding.Records.Catalog;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DataSeeding
{
    class Program
    {
        private static IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        static async Task Main(string[] args)
        {
            Console.WriteAscii("Data Seeder", Color.GreenYellow);

            var brands = await Parse<Brands>("brands.yaml");
            Console.WriteLineFormatted("Parsed {0} brand(s)", brands.Elements.Count(), Color.LightGoldenrodYellow, Color.Gray);

            var railways = await Parse<Railways>("railways.yaml");
            Console.WriteLineFormatted("Parsed {0} railway(s)", railways.Elements.Count(), Color.LightGoldenrodYellow, Color.Gray);

            var scales = await Parse<Scales>("scales.yaml");
            Console.WriteLineFormatted("Parsed {0} scale(s)", scales.Elements.Count(), Color.LightGoldenrodYellow, Color.Gray);

            var catalogItems = await Parse<CatalogItems>("catalogitems.yaml");
            Console.WriteLineFormatted("Parsed {0} catalog item(s)", catalogItems.Elements.Count(), Color.LightGoldenrodYellow, Color.Gray);

            Console.WriteLine("Press [ENTER] to quit.");
            Console.ReadLine();
        }

        static async Task<TResult> Parse<TResult>(string filename)
        {
            using var streamReader = new StreamReader(filename);
            string input = await streamReader.ReadToEndAsync();
            return deserializer.Deserialize<TResult>(input);
        }
    }
}
