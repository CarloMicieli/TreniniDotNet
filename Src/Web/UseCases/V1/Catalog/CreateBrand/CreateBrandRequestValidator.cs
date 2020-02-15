using FluentValidation;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
    {
        public CreateBrandRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.CompanyName)
                .MaximumLength(250);

            RuleFor(x => x.EmailAddress)
                .EmailAddress();
        }
    }
}
