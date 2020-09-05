using System;
using System.Net.Mail;
using NodaTime;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.SharedKernel.Addresses;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.TestHelpers.SeedData.Catalog
{
    public sealed class BrandsBuilder
    {
        private BrandId _brandId;
        private Slug? _slug;
        private string _name;
        private Uri _websiteUrl;
        private MailAddress _emailAddress;
        private string _companyName;
        private string _groupName;
        private string _description;
        private Address _address;
        private BrandKind _kind;
        private readonly Instant _created;
        private readonly Instant? _lastModified;
        private readonly int _version;

        internal BrandsBuilder()
        {
            _brandId = BrandId.NewId();
            _created = Instant.FromDateTimeUtc(DateTime.UtcNow);
            _lastModified = null;
            _version = 1;
        }

        public Brand Build()
        {
            return new Brand(_brandId, _slug ?? SharedKernel.Slugs.Slug.Of(_name), _name,
                _websiteUrl, _emailAddress,
                _companyName, _groupName, _description,
                _address, _kind,
                _created, _lastModified, _version);
        }

        public BrandsBuilder Id(Guid id)
        {
            _brandId = new BrandId(id);
            return this;
        }

        public BrandsBuilder Slug(Slug slug)
        {
            _slug = slug;
            return this;
        }

        public BrandsBuilder Name(string name)
        {
            _name = name;
            return this;
        }

        public BrandsBuilder WebsiteUrl(string websiteUrl) =>
            WebsiteUrl(new Uri(websiteUrl));

        public BrandsBuilder WebsiteUrl(Uri websiteUrl)
        {
            _websiteUrl = websiteUrl;
            return this;
        }

        public BrandsBuilder MailAddress(string mailAddress) =>
            MailAddress(new MailAddress(mailAddress));

        public BrandsBuilder MailAddress(MailAddress mailAddress)
        {
            _emailAddress = mailAddress;
            return this;
        }

        public BrandsBuilder CompanyName(string companyName)
        {
            _companyName = companyName;
            return this;
        }

        public BrandsBuilder GroupName(string groupName)
        {
            _groupName = groupName;
            return this;
        }

        public BrandsBuilder Description(string description)
        {
            _description = description;
            return this;
        }

        public BrandsBuilder Address(Address address)
        {
            _address = address;
            return this;
        }

        public BrandsBuilder Kind(BrandKind kind)
        {
            _kind = kind;
            return this;
        }

        public BrandsBuilder Industrial() => Kind(BrandKind.Industrial);

        public BrandsBuilder BrassModels() => Kind(BrandKind.BrassModels);
    }
}
