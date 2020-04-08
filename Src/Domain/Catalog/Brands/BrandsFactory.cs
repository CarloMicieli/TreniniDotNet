using System;
using System.Net.Mail;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Addresses;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using LanguageExt;
using static LanguageExt.Prelude;
using NodaTime;

namespace TreniniDotNet.Domain.Catalog.Brands
{
    public sealed class BrandsFactory : IBrandsFactory
    {
        private readonly IClock _clock;

        public BrandsFactory(IClock clock)
        {
            _clock = clock;
        }

        public Validation<Error, IBrand> NewBrandV(Guid brandId, string name, string? companyName, string? websiteUrl, string? emailAddress, string? brandKind)
        {
            var nameV = ToBrandName(name);
            var websiteV = ToUri(websiteUrl);
            var mailAddressV = ToMailAddress(emailAddress);
            var kindV = ToBrandKind(brandKind);

            return (nameV, websiteV, mailAddressV, kindV).Apply((_name, _website, _mail, _kind) =>
            {
                IBrand brand = new Brand(
                    new BrandId(brandId),
                    _name,
                    Slug.Of(_name),
                    companyName,
                    null,
                    null,
                    _website,
                    _mail,
                    _kind,
                    null,
                    _clock.GetCurrentInstant(),
                    1);
                return brand;
            });
        }

        public IBrand NewBrand(Guid brandId, string name, string slug, string? companyName, string? websiteUrl, string? emailAddress, string? brandKind)
        {
            return new Brand(
                id: new BrandId(brandId),
                name: name,
                slug: Slug.Of(slug),
                companyName: companyName,
                websiteUrl: !string.IsNullOrWhiteSpace(websiteUrl) ? new Uri(websiteUrl) : null,
                emailAddress: !string.IsNullOrWhiteSpace(emailAddress) ? new MailAddress(emailAddress) : null,
                kind: brandKind.ToBrandKind()
                );
        }

        public IBrand NewBrand(BrandId brandId,
            string name,
            Slug slug,
            string? companyName,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            BrandKind? kind)
        {
            return new Brand(
                id: brandId,
                name: name,
                slug: slug,
                companyName: companyName,
                websiteUrl: websiteUrl,
                emailAddress: mailAddress,
                kind: kind ?? BrandKind.Industrial
                );
        }

        public IBrand NewBrand(BrandId brandId,
            string name, Slug slug,
            string? companyName,
            string? groupName,
            string? description,
            Uri? websiteUrl,
            MailAddress? mailAddress,
            BrandKind? kind,
            Address? address)
        {
            return new Brand(
                id: brandId,
                name: name,
                slug: slug,
                companyName: companyName,
                groupName: groupName,
                description: description,
                websiteUrl: websiteUrl,
                emailAddress: mailAddress,
                kind: kind ?? BrandKind.Industrial,
                address: address,
                _clock.GetCurrentInstant(),
                1);
        }

        public static Validation<Error, string> ToBrandName(string? str) =>
            string.IsNullOrWhiteSpace(str) == false ? Success<Error, string>(str!) : Fail<Error, string>(Error.New("invalid brand: name cannot be empty"));

        public static Validation<Error, Uri> ToUri(string? str) =>
            str != null && Uri.TryCreate(str, UriKind.Absolute, out var uri) ?
                Success<Error, Uri>(uri) : Fail<Error, Uri>(Error.New("Invalid URI: The format of the URI could not be determined."));


        public static Validation<Error, MailAddress> ToMailAddress(string? str) =>
            str != null && MailAddress_TryCreate(str, out var mailAddress) ?
                Success<Error, MailAddress>(mailAddress!) : Fail<Error, MailAddress>(Error.New("The specified string is not in the form required for an e-mail address."));

        public static Validation<Error, BrandKind> ToBrandKind(string? str) =>
            str != null && Enum.TryParse<BrandKind>(str, true, out var kind) ?
                Success<Error, BrandKind>(kind) : Fail<Error, BrandKind>(Error.New("The specified string is not a valid brand kind."));

        private static bool MailAddress_TryCreate(string address, out MailAddress? result)
        {
            try
            {
                result = new MailAddress(address);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }
    }
}
