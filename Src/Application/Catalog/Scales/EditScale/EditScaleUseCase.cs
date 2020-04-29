using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public sealed class EditScaleUseCase : ValidatedUseCase<EditScaleInput, IEditScaleOutputPort>, IEditScaleUseCase
    {
        private readonly ScaleService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public EditScaleUseCase(IEditScaleOutputPort output,
            ScaleService scaleService,
            IUnitOfWork unitOfWork)
            : base(new EditScaleInputValidator(), output)
        {
            _scaleService = scaleService ??
                throw new ArgumentNullException(nameof(scaleService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditScaleInput input)
        {
            var scale = await _scaleService.GetBySlugAsync(input.ScaleSlug);
            if (scale is null)
            {
                OutputPort.ScaleNotFound(input.ScaleSlug);
                return;
            }

            var values = input.Values;

            ScaleGauge? scaleGauge = (values.Gauge.Inches is null && values.Gauge.Millimeters is null) ?
                (ScaleGauge?)null : values.Gauge.ToScaleGauge();

            var ratio = values.Ratio.HasValue ? Ratio.Of(values.Ratio.Value) : (Ratio?)null;

            await _scaleService.UpdateScale(
                scale,
                values.Name,
                ratio,
                scaleGauge,
                values.Description,
                ImmutableHashSet<ScaleStandard>.Empty,
                values.Weight);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditScaleOutput());
        }
    }
}
