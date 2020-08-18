using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSeeding.Clients.Extensions;
using DataSeeding.DataLoader.Records.Catalog.Scales;
using Google.Protobuf.Collections;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog.Core;
using TreniniDotNet.Catalog;

namespace DataSeeding.Clients.Catalog.Scales
{
    public sealed class ScalesClient
    {
        private ScalesService.ScalesServiceClient Client { get; }
        private Logger Log { get; }

        public ScalesClient(GrpcChannel channel, Logger log)
        {
            Log = log;
            Client = new ScalesService.ScalesServiceClient(channel);
        }

        public async Task<int> SendScalesAsync(IEnumerable<Scale> scales)
        {
            Log.Debug("Sending scales...");

            var requests = scales.Select(RequestFromRecord);

            try
            {
                var streamingClient = Client.CreateScales();

                foreach (var request in requests)
                {
                    Log.Debug("Sending {0}", request.Name);
                    await streamingClient.RequestStream.WriteAsync(request);
                }

                await streamingClient.RequestStream.CompleteAsync();

                var response = await streamingClient.ResponseAsync;
                Log.Debug("{0} scale(s) has been created", response.Created);
                return response.Created;
            }
            catch (RpcException e)
            {
                Log.Error(e.Message);
                return 0;
            }
        }

        private CreateScaleRequest RequestFromRecord(Scale scale)
        {
            var request = new CreateScaleRequest
            {
                Name = scale.Name.ToStringOrBlank(),
                Description = scale.Description.ToStringOrBlank(),
                Gauge = new ScaleGaugeRequest
                {
                    Inches = scale.Gauge.Inches,
                    Millimeters = scale.Gauge.Millimeters,
                    TrackGauge = scale.Gauge.TrackGauge.ToStringOrBlank()
                },
                Ratio = scale.Ratio,
                Weight = scale.Weight
            };

            request.Standard.AddRange(scale.Standards);

            return request;
        }
    }
}
