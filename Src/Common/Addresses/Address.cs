using System;
using System.Diagnostics.CodeAnalysis;

namespace TreniniDotNet.Common.Addresses
{
    public sealed class Address : IEquatable<Address>
    {
        public Address(string line1, string? line2, string city, string? region, string postalCode, string country)
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            Region = region;
            PostalCode = postalCode;
            Country = country;
        }

        public string Line1 { get; }
        public string? Line2 { get; }
        public string City { get; }

        // State / Province / Region
        public string? Region { get; }

        // ZIP/Postal Code
        public string PostalCode { get; }
        public string Country { get; }

        public static readonly Address NullAddress = new Address("", null, "", null, "", "");

        public static bool TryCreate(
            string? line1, string? line2,
            string? city,
            string? region,
            string? postalCode,
            string? country,
            [NotNullWhen(true)] out Address? result)
        {
            if (string.IsNullOrWhiteSpace(line1) == false &&
                string.IsNullOrWhiteSpace(city) == false &&
                string.IsNullOrWhiteSpace(postalCode) == false &&
                string.IsNullOrWhiteSpace(country) == false)
            {
                result = new Address(line1!, line2, city!, region, postalCode!, country!);
                return true;
            }

            if (string.IsNullOrWhiteSpace(line1) &&
                string.IsNullOrWhiteSpace(line2) &&
                string.IsNullOrWhiteSpace(city) &&
                string.IsNullOrWhiteSpace(region) &&
                string.IsNullOrWhiteSpace(postalCode) &&
                string.IsNullOrWhiteSpace(country))
            {
                result = NullAddress;
                return true;
            }

            result = default;
            return false;
        }

        public static Address? With(
            string? line1 = null,
            string? line2 = null,
            string? city = null,
            string? region = null,
            string? postalCode = null,
            string? country = null)
        {
            if (TryCreate(line1, line2, city, region, postalCode, country, out var a))
            {
                return a;
            }

            return null;
        }

        public override int GetHashCode() => HashCode.Combine(Line1, Line2, City, PostalCode, Region, Country);

        public static bool operator ==(Address left, Address right) => AreEquals(left, right);
        public static bool operator !=(Address left, Address right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is Address that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Address other) =>
            AreEquals(this, other);

        private static bool AreEquals(Address left, Address right) =>
            left.Line1 == right.Line1 &&
            left.Line2 == right.Line2 &&
            left.City == right.City &&
            left.PostalCode == right.PostalCode &&
            left.Region == right.Region &&
            left.Country == right.Country;
    }
}