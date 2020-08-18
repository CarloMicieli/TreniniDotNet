using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;

namespace TreniniDotNet.SharedKernel.Countries
{
    // A simple wrapper around country code, to ensure it allows only valid country codes
    public readonly struct Country : IEquatable<Country>
    {
        //  The Alpha-2 code (ISO 3166-1) for the current country
        public string Code { get; }
        public string EnglishName { get; }

        internal Country(RegionInfo info)
        {
            Code = info.TwoLetterISORegionName;
            EnglishName = info.EnglishName;
        }

        public static Country Of(string code)
        {
            if (code.Length != 2)
            {
                throw new ArgumentException(nameof(code));
            }

            Countries.TryGetCountry(code, out var c);
            return c;
        }

        public override string ToString() => $"{EnglishName}";

        public override bool Equals(object? obj)
        {
            return obj is Country other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public static bool operator ==(Country left, Country right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Country left, Country right)
        {
            return !(left == right);
        }

        public bool Equals(Country other)
        {
            return Code == other.Code;
        }
    }

    public static class Countries
    {
        private static readonly IImmutableDictionary<string, Country> countries = Init();

        public static IImmutableDictionary<string, Country> List() => countries;

        public static bool TryGetCountry(string code, out Country country)
        {
            if (countries.TryGetValue(code, out var c))
            {
                country = c;
                return true;
            }

            country = new Country(new RegionInfo(code));
            return true;
        }

        private static IImmutableDictionary<string, Country> Init()
        {
            var codes = new List<string>
            {
                "at",
                "be",
                "ca",
                "cn",
                "dk",
                "fi",
                "fr",
                "de",
                "it",
                "jp",
                "mx",
                "nl",
                "no",
                "pl",
                "ro",
                "ru",
                "es",
                "se",
                "ch",
                "tr",
                "gb",
                "us"
            };
            return codes
                .Select(it => new RegionInfo(it.ToUpper()))
                .Select(it => new Country(it))
                .ToImmutableDictionary(it => it.Code, it => it);
        }
    }
}
