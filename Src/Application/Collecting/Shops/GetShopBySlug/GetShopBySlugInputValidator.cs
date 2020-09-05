using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugInputValidator : AbstractUseCaseValidator<GetShopBySlugInput>
    {
        public GetShopBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty();
        }
    }
}
