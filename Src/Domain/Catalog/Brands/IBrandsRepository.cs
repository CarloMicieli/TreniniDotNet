using System.Threading.Tasks;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common;
using System;
using System.Net.Mail;
using System.Collections.Generic;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsRepository
    {
        Task<IBrand> GetBy(Slug slug);

        Task<BrandId> Add(BrandId brandId,
            string name,
            Slug slug,
            string? companyName,
            Uri? websiteUrl,
            MailAddress? emailAddress,
            BrandKind? brandKind);

        Task<bool> Exists(Slug slug);

        Task<List<IBrand>> GetAll();
    }
}
