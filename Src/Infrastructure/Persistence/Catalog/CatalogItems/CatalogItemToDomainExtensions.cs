using EfCatalogItem = TreniniDotNet.Infrastructure.Persistence.Catalog.CatalogItems.CatalogItem;
using DomainCatalogItem = TreniniDotNet.Domain.Catalog.CatalogItems.CatalogItem;
using System;

namespace Infrastructure.Persistence.Catalog.CatalogItems
{
    public static class CatalogItemToDomainExtensions
    {
        public static DomainCatalogItem ToDomain(this EfCatalogItem ci) 
        {
            throw new NotImplementedException("TODO");
        }
    }
}