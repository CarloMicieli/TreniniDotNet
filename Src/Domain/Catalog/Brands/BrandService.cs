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
            var newBrand = _brandsFactory.NewBrand(
                name,
                companyName,
                groupName,
                description,
                websiteUrl,
                emailAddress,
                kind,
                address);

            return _brandRepository.Add(newBrand);
        }

        public Task<PaginatedResult<IBrand>> FindAllBrands(Page? page)
        {
            return _brandRepository.GetBrands(page ?? Page.Default);
        }

        public Task<IBrand?> GetBrandBySlug(Slug slug)
        {
            return _brandRepository.GetBySlug(slug);
        }

        public Task<bool> BrandAlreadyExists(Slug slug)
        {
            return _brandRepository.Exists(slug);
        }
    }
}
