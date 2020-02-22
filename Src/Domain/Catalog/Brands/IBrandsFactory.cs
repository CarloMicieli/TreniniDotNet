﻿using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public interface IBrandsFactory
    {
        IBrand NewBrand(Guid brandId, 
            string name, 
            string slug, 
            string? companyName,
            string? websiteUrl,
            string? emailAddress,
            string? brandKind);

        IBrand NewBrand(BrandId brandId, 
            string name, 
            Slug slug, 
            string? companyName, 
            Uri? uri, 
            MailAddress? mailAddress, 
            BrandKind? industrial);
    }
}
