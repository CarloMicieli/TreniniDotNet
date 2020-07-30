using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsFactory : AggregateRootFactory<BrandId, Brand>
    {
        public BrandsFactory(IClock clock, IGuidSource guidSource)
            : base(clock, guidSource)
        {
        }

        public Brand CreateBrand(string name,
            string? companyName, string? groupName, string? description,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            BrandKind kind,
            Address? address)
        {
            var slug = Slug.Of(name);

            return new Brand(
                NewId(id => new BrandId(id)),
                slug,
                name,
                websiteUrl,
                mailAddress,
                companyName,
                groupName,
                description,
                address,
                kind,
                GetCurrentInstant(),
                null,
                1);
        }
    }
}
