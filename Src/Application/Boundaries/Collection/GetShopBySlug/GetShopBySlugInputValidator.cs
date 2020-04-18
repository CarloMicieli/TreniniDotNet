using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.GetShopBySlug
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
