using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public sealed class EditScaleUseCase : AbstractUseCase<EditScaleInput, EditScaleOutput, IEditScaleOutputPort>
    {
        private readonly ScalesService _scaleService;
        private readonly IUnitOfWork _unitOfWork;

        public EditScaleUseCase(
            IUseCaseInputValidator<EditScaleInput> inputValidator,
            IEditScaleOutputPort output,
            ScalesService scaleService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
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

            var scaleGauge = (values.Gauge.Inches is null && values.Gauge.Millimeters is null) ?
                null : values.Gauge.ToScaleGauge();

            var ratio = values.Ratio.HasValue ? Ratio.Of(values.Ratio.Value) : (Ratio?)null;
            
            var standards = values.Standards
                .Select(EnumHelpers.OptionalValueFor<ScaleStandard>)
                .Where(it => it.HasValue)
                .Select(it => it!.Value)
                .ToHashSet();
            
            var modifiedScale = scale.With(
                values.Name,
                ratio,
                scaleGauge,
                values.Description,
                standards,
                values.Weight);

            await _scaleService.UpdateScaleAsync(modifiedScale);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditScaleOutput());
        }
    }
}
