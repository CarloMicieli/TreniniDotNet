using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataSeeding.Clients.Extensions;
using DataSeeding.DataLoader.Records.Catalog.Railways;
using Grpc.Core;
using Grpc.Net.Client;
using Serilog.Core;
using TreniniDotNet.Catalog;

namespace DataSeeding.Clients.Catalog.Railways
{
    public sealed class RailwaysClient
    {
        private RailwaysService.RailwaysServiceClient Client { get; }
        private Logger Log { get; }

        public RailwaysClient(GrpcChannel channel, Logger log)
        {
            Log = log;
            Client = new RailwaysService.RailwaysServiceClient(channel);
        }

        public async Task<int> SendRailwaysAsync(IEnumerable<Railway> railways)
        {
            var requests = railways.Select(RequestFromRecord);

            Log.Debug("Sending railways...");

            try
            {
                var streamingClient = Client.CreateRailways();

                foreach (var request in requests)
                {
                    Log.Debug("Sending railway {0}", request.Name);
                    await streamingClient.RequestStream.WriteAsync(request);
                }

                await streamingClient.RequestStream.CompleteAsync();
                var response = await streamingClient.ResponseAsync;
                Log.Debug("{0} railway(s) has been created", response.Created);
                return response.Created;
            }
            catch (RpcException e)
            {
                Log.Error(e.Message);
                return 0;
            }
        }

        private CreateRailwayRequest RequestFromRecord(Railway railway)
        {
            return new CreateRailwayRequest
            {
                Name = railway.Name.ToStringOrBlank(),
                Country = railway.Country.ToStringOrBlank(),
                CompanyName = railway.CompanyName.ToStringOrBlank(),
                WebsiteUrl = railway.WebsiteUrl.ToStringOrBlank(),
                Headquarters = railway.Headquarters.ToStringOrBlank(),
                PeriodOfActivity = PeriodOfActivity(railway),
                Gauge = RailwayGauge(railway),
                TotalLength = TotalRailwayLength(railway)
            };
        }

        private static PeriodOfActivityRequest PeriodOfActivity(Railway railway)
        {
            return new PeriodOfActivityRequest
            {
                Status = railway.PeriodOfActivity.Status.ToStringOrBlank(),
                OperatingSince = railway.PeriodOfActivity.OperatingSince.ToTimestamp(),
                OperatingUntil = railway.PeriodOfActivity.OperatingUntil.ToTimestamp(),
            };
        }

        private static RailwayGaugeRequest RailwayGauge(Railway railway)
        {
            return new RailwayGaugeRequest
            {
                Inches = railway.RailwayGauge.Inches,
                Millimeters = railway.RailwayGauge.Millimeters,
                TrackGauge = railway.RailwayGauge.TrackGauge.ToStringOrBlank()
            };
        }

        private static TotalRailwayLengthRequest TotalRailwayLength(Railway railway)
        {
            return new TotalRailwayLengthRequest
            {
                Kilometers = railway.Length.Kilometers,
                Miles = railway.Length.Miles
            };
        }
    }
}
