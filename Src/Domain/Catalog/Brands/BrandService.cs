using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TreniniDotNet.Common;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandService
    {
        private readonly IBrandsRepository _brandRepository;
        private readonly IBrandsFactory _brandFactory;

        public BrandService(IBrandsRepository brandRepository, IBrandsFactory brandFactory)
        {
            _brandRepository = brandRepository;
            _brandFactory = brandFactory;
        }

        public async Task<IBrand> CreateBrand(string name, string? companyName, string? websiteUrl, string? emailAddress, BrandKind kind)
        {
            var brand = _brandFactory.NewBrand(name, companyName, websiteUrl, emailAddress, kind);
            await _brandRepository.Add(brand);
            return brand;
        }

        public async Task<IBrand> CreateBrand(string name, string? companyName, Uri? websiteUrl, MailAddress? emailAddress, BrandKind kind)
        {
            var brand = _brandFactory.NewBrand(name, companyName, websiteUrl, emailAddress, kind);
            await _brandRepository.Add(brand);
            return brand;
        }

        public Task<IBrand> GetBy(Slug slug)
        {
            return _brandRepository.GetBy(slug);
        }

        public async Task<bool> BrandAlreadyExists(string name)
        {
            try
            {
                var slug = Slug.Of(name);
                var brand = await _brandRepository.GetBy(slug);
                return true;
            }
            catch (BrandNotFoundException)
            {
                return false;
            }
        }
    }
}
