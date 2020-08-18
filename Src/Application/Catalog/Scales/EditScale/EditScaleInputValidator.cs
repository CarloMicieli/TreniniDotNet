using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public sealed class EditScaleInputValidator : AbstractUseCaseValidator<EditScaleInput>
    {
        public EditScaleInputValidator()
        {
            RuleFor(x => x.ScaleSlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedScaleValuesValidator());
        }
    }
}
