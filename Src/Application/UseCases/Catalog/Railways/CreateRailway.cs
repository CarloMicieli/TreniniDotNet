using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog.Railways
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

            var railwayLength = input.TotalLength.ToRailwayLength();
            var periodOfActivity = input.PeriodOfActivity.ToPeriodOfActivity();
            var railwayGauge = input.Gauge.ToRailwayGauge();

            var websiteUrl = input.WebsiteUrl.ToUriOpt();
            var country = Country.Of(input.Country!);

            var _ = await _railwayService.CreateRailway(
                railwayName,
                slug,
                input.CompanyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                websiteUrl,
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
