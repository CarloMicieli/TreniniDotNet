using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Shops.GetShopBySlug
{
    public sealed class GetShopBySlugInputValidator : AbstractValidator<GetShopBySlugInput>
    {
        public GetShopBySlugInputValidator()
        {
            RuleFor(x => x.Slug)
                .NotEmpty();
        }
    }
}
