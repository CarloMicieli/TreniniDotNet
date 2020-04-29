using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public sealed class EditRailwayUseCase : ValidatedUseCase<EditRailwayInput, IEditRailwayOutputPort>, IEditRailwayUseCase
    {
        private readonly RailwayService _railwayService;
        private readonly IUnitOfWork _unitOfWork;

        public EditRailwayUseCase(IEditRailwayOutputPort output,
            RailwayService railwayService,
            IUnitOfWork unitOfWork)
            : base(new EditRailwayInputValidator(), output)
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

            await _railwayService.UpdateRailway(
                railway,
                values.Name,
                values.CompanyName,
                country,
                periodOfActivity,
                railwayLength,
                railwayGauge,
                websiteUrl,
                values.Headquarters);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditRailwayOutput());
        }
    }
}
