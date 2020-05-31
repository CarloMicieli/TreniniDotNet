using System;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using TreniniDotNet.Application.Catalog.Scales;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Catalog;
using TreniniDotNet.GrpcServices.Extensions;

namespace TreniniDotNet.GrpcServices.Catalog.Scales
{
    public sealed class GrpcScalesService : ScalesService.ScalesServiceBase
    {
        public GrpcScalesService(
            ICreateScaleUseCase useCase,
            CreateScalePresenter presenter,
            ILogger<GrpcScalesService> log)
        {
            Log = log ??
                throw new ArgumentNullException(nameof(log));
            Presenter = presenter ??
                throw new ArgumentNullException(nameof(presenter));
            UseCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        private ILogger<GrpcScalesService> Log { get; }
        private CreateScalePresenter Presenter { get; }
        private ICreateScaleUseCase UseCase { get; }

        public override async Task<CreateScaleResponse> CreateScale(CreateScaleRequest request, ServerCallContext context)
        {
            await UseCase.Execute(InputFromRequest(request));
            return Presenter.Response;
        }

        public override async Task<CreateScalesResponse> CreateScales(IAsyncStreamReader<CreateScaleRequest> requestStream, ServerCallContext context)
        {
            var created = 0;

            while (await requestStream.MoveNext())
            {
                var input = InputFromRequest(requestStream.Current);
                try
                {
                    await UseCase.Execute(input);
                    Log.LogInformation("Scale {0} has been created (slug: {1})", input.Name, Presenter.Response.Slug);
                    created++;
                }
                catch (RpcException rpcEx)
                {
                    Log.LogError(rpcEx.Message);
                }
            }

            return new CreateScalesResponse { Created = created };
        }

        private static CreateScaleInput InputFromRequest(CreateScaleRequest request)
        {
            return new CreateScaleInput(
                request.Name.ToNullableString(),
                request.Ratio.ToDecimal(),
                request.Gauge.ToScaleGaugeInput(),
                request.Description.ToNullableString(),
                request.Standard.ToList(),
                request.Weight);
        }
    }

    public static class ScaleGaugeRequestExtensions
    {
        public static ScaleGaugeInput ToScaleGaugeInput(this ScaleGaugeRequest req)
        {
            return new ScaleGaugeInput(
                req.TrackGauge.ToNullableString(),
                req.Inches.ToNullableDecimal(),
                req.Millimeters.ToNullableDecimal());
        }
    }
}
