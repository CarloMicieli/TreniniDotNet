using System;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Application.Catalog.Railways;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Extensions;

namespace TreniniDotNet.GrpcServices.Catalog.Railways
{
    public sealed class GrpcRailwaysService : RailwaysService.RailwaysServiceBase
    {
        public GrpcRailwaysService(
            ICreateRailwayUseCase useCase,
            CreateRailwayPresenter presenter,
            ILogger<GrpcRailwaysService> log)
        {
            Log = log ??
                throw new ArgumentNullException(nameof(log));
            Presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
            UseCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        private ILogger<GrpcRailwaysService> Log { get; }
        private CreateRailwayPresenter Presenter { get; }
        private ICreateRailwayUseCase UseCase { get; }

        public override async Task<CreateRailwayResponse> CreateRailway(CreateRailwayRequest request, ServerCallContext context)
        {
            await UseCase.Execute(InputFromRequest(request));
            return Presenter.Response;
        }

        public override async Task<CreateRailwaysResponse> CreateRailways(IAsyncStreamReader<CreateRailwayRequest> requestStream, ServerCallContext context)
        {
            var created = 0;

            while (await requestStream.MoveNext())
            {
                var input = InputFromRequest(requestStream.Current);
                try
                {
                    await UseCase.Execute(input);
                    Log.LogInformation("Railway {0} created (slug: {1})", input.Name, Presenter.Response.Slug);
                    created++;
                }
                catch (RpcException rpcEx)
                {
                    Log.LogError("Railway {0}, error: {1}", input.Name, rpcEx.Message);
                }
            }

            return new CreateRailwaysResponse
            {
                Created = created
            };
        }

        private static CreateRailwayInput InputFromRequest(CreateRailwayRequest request)
        {
            return new CreateRailwayInput(
                request.Name.ToNullableString(),
                request.CompanyName.ToNullableString(),
                request.Country.ToNullableString(),
                request.PeriodOfActivity.ToPeriodActivityInput(),
                request.TotalLength.ToTotalRailwayLengthInput(),
                request.Gauge.ToRailwayGaugeInput(),
                request.WebsiteUrl.ToNullableString(),
                request.Headquarters.ToNullableString());
        }
    }

    public static class PeriodOfActivityRequestExtensions
    {
        public static PeriodOfActivityInput ToPeriodActivityInput(this PeriodOfActivityRequest req)
        {
            return new PeriodOfActivityInput(
                req.Status.ToNullableString(),
                req.OperatingSince?.ToDateTime(),
                req.OperatingUntil?.ToDateTime());
        }
    }

    public static class TotalLengthRequestExtensions
    {
        public static TotalRailwayLengthInput ToTotalRailwayLengthInput(this TotalRailwayLengthRequest req)
        {
            return new TotalRailwayLengthInput(req.Kilometers, req.Miles);
        }
    }

    public static class RailwayGaugeRequestExtensions
    {
        public static RailwayGaugeInput ToRailwayGaugeInput(this RailwayGaugeRequest req)
        {
            return new RailwayGaugeInput(
                req.TrackGauge,
                req.Millimeters.ToNullableDecimal(),
                req.Inches.ToNullableDecimal());
        }
    }
}
