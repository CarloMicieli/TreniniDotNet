using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data.Pagination;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsService : IDomainService
    {
        private readonly IBrandsRepository _brandRepository;
        private readonly BrandsFactory _brandsFactory;

        public BrandsService(IBrandsRepository brandRepository, BrandsFactory brandsFactory)
        {
            _brandRepository = brandRepository ?? throw new ArgumentNullException(nameof(brandRepository));
            _brandsFactory = brandsFactory ?? throw new ArgumentNullException(nameof(brandsFactory));
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
            var newBrand = _brandsFactory.CreateBrand(
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

        public Task<bool> BrandAlreadyExists(Slug slug)
        {
            return _brandRepository.ExistsAsync(slug);
        }

        public Task<Brand?> GetBrandBySlug(Slug slug) => _brandRepository.GetBySlugAsync(slug);

        public Task UpdateBrand(Brand brand) => _brandRepository.UpdateAsync(brand);

        public Task<PaginatedResult<Brand>> FindAllBrands(Page page) => _brandRepository.GetAllAsync(page);
    }
}
