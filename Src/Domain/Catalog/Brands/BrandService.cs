using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Pagination;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandService
    {
        private readonly IBrandsRepository _brandRepository;
        private readonly IBrandsFactory _brandsFactory;

        public BrandService(IBrandsRepository brandRepository, IBrandsFactory brandsFactory)
        {
            _brandRepository = brandRepository;
            _brandsFactory = brandsFactory;
        }

        public Task<BrandId> CreateBrand(
            string name,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind,
            Address? address)
        {
            var newBrand = _brandsFactory.CreateNewBrand(
                name,
                companyName,
                groupName,
                description,
                websiteUrl,
                emailAddress,
                kind,
                address);

            return _brandRepository.AddAsync(newBrand);
        }

        public Task<PaginatedResult<IBrand>> FindAllBrands(Page? page)
        {
            return _brandRepository.GetBrandsAsync(page ?? Page.Default);
        }

        public Task<IBrand?> GetBrandBySlug(Slug slug)
        {
            return _brandRepository.GetBySlugAsync(slug);
        }

        public Task<bool> BrandAlreadyExists(Slug slug)
        {
            return _brandRepository.ExistsAsync(slug);
        }

        public Task UpdateBrand(IBrand brand,
            string? name,
            BrandKind? brandKind,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            Address? address)
        {
            var updated = _brandsFactory.UpdateBrand(brand,
                name,
                brandKind,
                companyName,
                groupName,
                description,
                websiteUrl,
                mailAddress,
                address);
            return _brandRepository.UpdateAsync(updated);
        }
    }
}
