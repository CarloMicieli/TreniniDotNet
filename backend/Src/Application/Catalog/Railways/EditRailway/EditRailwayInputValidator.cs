using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Railways.EditRailway
{
    public sealed class EditRailwayInputValidator : AbstractUseCaseValidator<EditRailwayInput>
    {
        public EditRailwayInputValidator()
        {
            RuleFor(x => x.RailwaySlug)
                .ValidSlug();

            RuleFor(x => x.Values)
                .SetValidator(new ModifiedRailwayValuesValidator());
        }
    }
}
