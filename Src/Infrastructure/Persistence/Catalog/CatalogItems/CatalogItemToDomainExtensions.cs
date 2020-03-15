using EfCatalogItem = TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem;
using DomainCatalogItem = TreniniDotNet.Domain.Catalog.CatalogItems.CatalogItem;
using System;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Brands;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;

namespace Infrastructure.Persistence.Catalog.CatalogItems
{
    public static class CatalogItemToDomainExtensions
    {
        public static DomainCatalogItem ToDomain(this EfCatalogItem ci) 
        {
            IBrand brand = new Brand(
                new BrandId(ci.Brand.BrandId), 
                ci.Brand.Name, 
                Slug.Of(ci.Brand.Slug),
                ci.Brand.CompanyName,
                ci.Brand.WebsiteUrl != null ? new Uri(ci.Brand.WebsiteUrl) : null, 
                ci.Brand.EmailAddress != null ? new System.Net.Mail.MailAddress(ci.Brand.EmailAddress) : null, 
                ci.Brand.BrandKind.ToBrandKind());

            IScale scale = new Scale(
                new ScaleId(ci.Scale.ScaleId),
                Slug.Of(ci.Scale.Slug),
                ci.Scale.Name,
                Ratio.Of(ci.Scale.Ratio),
                Gauge.OfMillimiters(ci.Scale.Gauge),
                ci.Scale.TrackGauge.ToTrackGauge(),
                ci.Scale.Notes);

            return new DomainCatalogItem(
                new CatalogItemId(ci.CatalogItemId),
                brand,
                new ItemNumber(ci.ItemNumber),
                Slug.Of(ci.Slug),
                scale,
                new List<RollingStock>(),
                ci.PowerMethod.ToPowerMethod() ?? PowerMethod.DC,
                ci.Description,
                ci.PrototypeDescription,
                ci.ModelDescription);
        }
    }
}