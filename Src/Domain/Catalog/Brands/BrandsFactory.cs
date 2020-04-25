using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using static TreniniDotNet.Common.Enums.EnumHelpers;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsFactory : IBrandsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public BrandsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));
            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }

        public IBrand NewBrand(
            string name,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind kind,
            Address? address)
        {
            var brandId = new BrandId(_guidSource.NewGuid());
            Slug slug = Slug.Of(name);

            return new Brand(
                brandId,
                name,
                slug,
                companyName,
                groupName,
                description,
                websiteUrl,
                emailAddress,
                kind,
                address,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IBrand NewBrand(Guid brandId,
            string name, string slug,
            string kind,
            string? companyName, string? groupName,
            string? description,
            string? websiteUrl,
            string? mailAddress,
            Address? address,
            DateTime created,
            DateTime? modified,
            int version)
        {
            return new Brand(
                new BrandId(brandId),
                name,
                Slug.Of(slug),
                companyName,
                groupName,
                description,
                websiteUrl.ToUriOpt(),
                mailAddress.ToMailAddressOpt(),
                RequiredValueFor<BrandKind>(kind),
                address,
                created.ToUtc(),
                null,
                version);
        }

        public IBrand NewBrandWith(
            BrandId brandId,
            string name,
            Slug? slug = null,
            BrandKind? kind = null,
            string? companyName = null,
            string? groupName = null,
            string? description = null,
            Uri? website = null,
            MailAddress? mailAddress = null,
            Address? address = null)
        {
            return new Brand(
                brandId,
                name,
                slug ?? Slug.Of(name),
                companyName,
                groupName,
                description,
                website,
                mailAddress,
                kind ?? BrandKind.Industrial,
                address,
                _clock.GetCurrentInstant(),
                null,
                1);
        }

        public IBrand UpdateBrand(IBrand brand,
            string? name,
            BrandKind? brandKind,
            string? companyName,
            string? groupName,
            string? description,
            Uri? website,
            MailAddress? mailAddress,
            Address? address)
        {
            var slug = (name is null) ? brand.Slug : Slug.Of(name);

            return new Brand(
                brand.BrandId,
                name ?? brand.Name,
                slug,
                companyName ?? brand.CompanyName,
                groupName ?? brand.GroupName,
                description ?? brand.Description,
                website ?? brand.WebsiteUrl,
                mailAddress ?? brand.EmailAddress,
                brandKind ?? brand.Kind,
                address ?? brand.Address,
                brand.CreatedDate,
                _clock.GetCurrentInstant(),
                brand.Version + 1);
        }
    }
}
