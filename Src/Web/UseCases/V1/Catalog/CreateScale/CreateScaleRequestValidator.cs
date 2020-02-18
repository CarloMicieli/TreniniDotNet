using FluentValidation;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateScale
{
    public class CreateScaleRequestValidator : AbstractValidator<CreateScaleRequest>
    {
        public CreateScaleRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(10);

            RuleFor(x => x.Gauge)
                .GreaterThanOrEqualTo(0M);

            RuleFor(x => x.Ratio)
                .GreaterThanOrEqualTo(0M);
        }
    }
}