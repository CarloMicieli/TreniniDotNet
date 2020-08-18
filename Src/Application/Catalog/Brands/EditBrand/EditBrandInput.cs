using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Brands.EditBrand
{
    public sealed class EditBrandInput : IUseCaseInput
    {
        public EditBrandInput(
            Slug brandSlug,
            string? name,
            string? companyName,
            string? groupName,
            string? description,
            string? websiteUrl,
            string? emailAddress,
            string? brandType,
            AddressInput? address)
        {
            BrandSlug = brandSlug;
            Values = new ModifiedBrandValues(
                name,
                companyName,
                groupName,
                description,
                websiteUrl,
                emailAddress,
                brandType,
                address);
        }

        public Slug BrandSlug { get; }
        public ModifiedBrandValues Values { get; }
    }
}
