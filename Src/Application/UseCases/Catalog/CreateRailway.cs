using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateRailway : ValidatedUseCase<CreateRailwayInput, ICreateRailwayOutputPort>, ICreateRailwayUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwayService _railwayService;

        public CreateRailway(
            ICreateRailwayOutputPort outputPort,
            RailwayService railwayService,
            IUnitOfWork unitOfWork)
            : base(new CreateRailwayInputValidator(), outputPort)
        {
            _unitOfWork = unitOfWork;
            _railwayService = railwayService;
        }

        protected override async Task Handle(CreateRailwayInput input)
        {
            var railwayName = input.Name!;
            var slug = Slug.Of(railwayName);

            bool exists = await _railwayService.RailwayAlreadyExists(slug);
            if (exists)
            {
                OutputPort.RailwayAlreadyExists($"Railway '{railwayName}' already exists");
                return;
            }

            var (status, since, until) = input.PeriodOfActivity;
            var periodOfActivity = PeriodOfActivity.Of(status, since, until);
            
            var country = Country.Of(input.Country!);

            var (km, mi) = input.TotalLength;
            var railwayLength = RailwayLength.TryCreate(km, mi, out var rl) ? rl : null;

            var website = Uri.TryCreate(input.WebsiteUrl, UriKind.Absolute, out var uri) ? uri : null;

            var (trackGauge, mm, inches) = input.Gauge;
            var railwayGauge = RailwayGauge.TryCreate(trackGauge, inches, mm, out var rg) ? rg : null;

            var _ = await _railwayService.CreateRailway(
                railwayName,
                slug,
                input.CompanyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                website,
                input.Headquarters);

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateRailwayOutput(slug);
            OutputPort.Standard(output);
        }
    }
}
