using System;
using System.Linq;
using System.Threading.Tasks;
using CommandLine;
using DataSeeding.Clients;
using DataSeeding.DataLoader;
using DataSeeding.DataLoader.Records.Catalog.Brands;
using DataSeeding.DataLoader.Records.Catalog.CatalogItems;
using DataSeeding.DataLoader.Records.Catalog.Railways;
using DataSeeding.DataLoader.Records.Catalog.Scales;
using Serilog;
using Serilog.Core;

namespace DataSeeding
{
    class Program
    {
        public class Options
        {
            [Option('u', "url", Required = true, HelpText = "The endpoint uri.", Default = "https://localhost:5001")]
            public Uri EndpointUri { get; set; }

            [Option('i', "input", Required = true, HelpText = "The input file.")]
            public string InputFile { get; set; }

            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool Verbose { get; set; }
        }

        [Verb("brands", HelpText = "Send brands")]
        public class BrandsOptions : Options
        {
        }

        [Verb("catalogItems", HelpText = "Send catalog items")]
        public class CatalogItemsOptions : Options
        {
        }

        [Verb("railways", HelpText = "Send railways")]
        public class RailwaysOptions : Options
        {
        }

        [Verb("scales", HelpText = "Send scales")]
        public class ScalesOptions : Options
        {
        }

        static async Task Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            await Parser.Default.ParseArguments<BrandsOptions, CatalogItemsOptions, RailwaysOptions, ScalesOptions>(args)
                .MapResult(
                    (BrandsOptions o) => SendBrands(o, log),
                    (CatalogItemsOptions o) => SendCatalogItems(o, log),
                    (RailwaysOptions o) => SendRailways(o, log),
                    (ScalesOptions o) => SendScales(o, log),
                    errs => Task.FromResult(0));
        }

        private static async Task<int> SendBrands(BrandsOptions o, Logger log)
        {
            var catalogClient = new CatalogClient(o.EndpointUri.ToString(), log);
            var brands = await YamlParser.Parse<Brands>(o.InputFile);
            if (o.Verbose)
            {
                log.Information("Parsed {0} brand(s)", brands.Elements.Count());
            }

            var created = await catalogClient.Brands.SendBrandsAsync(brands.Elements);
            return created;
        }

        private static async Task<int> SendCatalogItems(CatalogItemsOptions o, Logger log)
        {
            var catalogClient = new CatalogClient(o.EndpointUri.ToString(), log);
            var catalogItems = await YamlParser.Parse<CatalogItems>(o.InputFile);

            if (o.Verbose)
            {
                log.Information("Parsed {0} catalog item(s)", catalogItems.Elements.Count());
            }

            var created = await catalogClient.CatalogItemsClient.SendCatalogItemsAsync(catalogItems.Elements);
            return created;
        }

        private static async Task<int> SendRailways(RailwaysOptions o, Logger log)
        {
            var catalogClient = new CatalogClient(o.EndpointUri.ToString(), log);
            var railways = await YamlParser.Parse<Railways>(o.InputFile);

            if (o.Verbose)
            {
                log.Information("Parsed {0} railway(s)", railways.Elements.Count());
            }

            var created = await catalogClient.Railways.SendRailwaysAsync(railways.Elements);
            return created;
        }

        private static async Task<int> SendScales(ScalesOptions o, Logger log)
        {
            var catalogClient = new CatalogClient(o.EndpointUri.ToString(), log);
            var scales = await YamlParser.Parse<Scales>(o.InputFile);

            if (o.Verbose)
            {
                log.Information("Parsed {0} scales(s)", scales.Elements.Count());
            }

            var created = await catalogClient.Scales.SendScalesAsync(scales.Elements);
            return created;
        }
    }
}
