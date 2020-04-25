using NodaTime;
using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.Catalog.Brands
{
    public class FakeBrand : IBrand
    {
        public Uri WebsiteUrl { set; get; }

        public MailAddress EmailAddress { set; get; }

        public string CompanyName { set; get; }

        public string GroupName { set; get; }

        public string Description { set; get; }

        public BrandKind Kind { set; get; }

        public Address Address { set; get; }

        public BrandId BrandId { set; get; }

        public Slug Slug { set; get; }

        public string Name { set; get; }

        public Instant CreatedDate { set; get; }

        public Instant? ModifiedDate { set; get; }

        public int Version { set; get; }

        public FakeBrand()
        {
            BrandId = new BrandId(new Guid("2dfc8a61-8218-44af-8be5-d012bde4cf03"));
            Slug = Slug.Of("acme");
            Name = "A.C.M.E.";
            WebsiteUrl = new Uri("http://localhost");
            EmailAddress = new MailAddress("mail@mail.com");
            CompanyName = "Associazione Costruzioni Modellistiche Esatte";
            GroupName = null;
            Description = null;
            Address = null;
            Kind = BrandKind.Industrial;
            CreatedDate =
                        Instant.FromDateTimeUtc(DateTime.SpecifyKind(new DateTime(1988, 11, 25), DateTimeKind.Utc));
            ModifiedDate = null;
            Version = 1;
        }

        public IBrand With(string companyName = null)
        {
            this.CompanyName = companyName;
            return this;
        }

        public IBrandInfo ToBrandInfo() => this;
    }
}
