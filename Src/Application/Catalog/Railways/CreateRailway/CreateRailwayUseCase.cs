using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Countries;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.CreateRailway
{
    public sealed class CreateRailwayUseCase : AbstractUseCase<CreateRailwayInput, CreateRailwayOutput, ICreateRailwayOutputPort>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwaysService _railwayService;

        public CreateRailwayUseCase(
            IUseCaseInputValidator<CreateRailwayInput> inputValidator,
            ICreateRailwayOutputPort outputPort,
            RailwaysService railwayService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _railwayService = railwayService;
            _unitOfWork = unitOfWork;
        }

        protected override async Task Handle(CreateRailwayInput input)
        {
            var railwayName = input.Name!;
            var slug = Slug.Of(railwayName);

            bool exists = await _railwayService.RailwayAlreadyExists(slug);
            if (exists)
            {
                OutputPort.RailwayAlreadyExists(slug);
                return;
            }

            var railwayLength = input.TotalLength.ToRailwayLength();
            var periodOfActivity = input.PeriodOfActivity.ToPeriodOfActivity();
            var railwayGauge = input.Gauge.ToRailwayGauge();

            var websiteUrl = input.WebsiteUrl.ToUriOpt();
            var country = Country.Of(input.Country!);

            var _ = await _railwayService.CreateRailway(
                railwayName,
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
