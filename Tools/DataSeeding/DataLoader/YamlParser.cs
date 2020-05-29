using System.IO;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DataSeeding.DataLoader
{
    public static class YamlParser
    {
        private static IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance)
            .Build();

        public static async Task<TResult> Parse<TResult>(string filename)
        {
            using var streamReader = new StreamReader(filename);
            var input = await streamReader.ReadToEndAsync();
            return deserializer.Deserialize<TResult>(input);
        }
    }
}
