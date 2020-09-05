using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.SharedKernel.Countries;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public sealed class EditRailwayUseCase : AbstractUseCase<EditRailwayInput, EditRailwayOutput, IEditRailwayOutputPort>
    {
        private readonly RailwaysService _railwayService;
        private readonly IUnitOfWork _unitOfWork;

        public EditRailwayUseCase(
            IUseCaseInputValidator<EditRailwayInput> inputValidator,
            IEditRailwayOutputPort outputPort,
            RailwaysService railwayService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, outputPort)
        {
            _railwayService = railwayService ??
                throw new ArgumentNullException(nameof(railwayService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditRailwayInput input)
        {
            var railway = await _railwayService.GetBySlugAsync(input.RailwaySlug);
            if (railway is null)
            {
                OutputPort.RailwayNotFound(input.RailwaySlug);
                return;
            }

            var values = input.Values;

            var websiteUrl = values.WebsiteUrl.ToUriOpt();
            var country = (values.Country is null) ?
                (Country?)null : Country.Of(values.Country);

            var railwayGauge = values.Gauge.ToRailwayGauge();
            var railwayLength = values.TotalLength.ToRailwayLength();
            var periodOfActivity = values.PeriodOfActivity.ToPeriodOfActivity();

            var modifiedRailway = railway.With(
                values.Name,
                values.CompanyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                websiteUrl,
                values.Headquarters);

            await _railwayService.UpdateRailway(modifiedRailway);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditRailwayOutput());
        }
    }
}
