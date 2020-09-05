using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TreniniDotNet.Domain.Catalog.CatalogItems.RollingStocks
{
    public sealed class ServiceLevel
    {
        public static ServiceLevel FirstClass = new ServiceLevel("1cl");
        public static ServiceLevel SecondClass = new ServiceLevel("2cl");
        public static ServiceLevel FirstSecondClass = new ServiceLevel("1cl/2cl");
        public static ServiceLevel ThirdClass = new ServiceLevel("3cl");
        public static ServiceLevel SecondThirdClass = new ServiceLevel("2cl/3cl");

        private static readonly IReadOnlyDictionary<string, ServiceLevel> _cachedValues;

        private readonly string _value;

        private ServiceLevel(string value)
        {
            _value = value;
        }

        static ServiceLevel()
        {
            _cachedValues = new Dictionary<string, ServiceLevel>
            {
                { FirstClass.ToString().ToUpper(), FirstClass },
                { FirstSecondClass.ToString().ToUpper(), FirstSecondClass },
                { SecondClass.ToString().ToUpper(), SecondClass },
                { ThirdClass.ToString().ToUpper(), ThirdClass },
                { SecondThirdClass.ToString().ToUpper(), SecondThirdClass }
            };
        }

        public static ServiceLevel Parse(string str)
        {
            if (TryParse(str, out var serviceLevel))
            {
                return serviceLevel;
            }

            string allowedValues = string.Join(", ",
                _cachedValues.Values
                    .Select(it => it.ToString())
                    .Where(it => it.IndexOf('/') == -1));
            throw new ArgumentOutOfRangeException(nameof(str), $"The value is not a valid Service Level [Allowed values are: {allowedValues}]");
        }

        public static bool TryParse(string str, [NotNullWhen(true)] out ServiceLevel? serviceLevel)
        {
            if (_cachedValues.TryGetValue(str.ToUpper(), out var sl))
            {
                serviceLevel = sl;
                return true;
            }

            if (str.IndexOf('/') != -1)
            {
                var tokens = str.Split('/')
                    .OrderBy(it => it)
                    .Distinct()
                    .Select(it =>
                        _cachedValues.TryGetValue(it.ToUpper(), out var level)
                            ? (isValid: true, value: level.ToString())
                            : (isValid: false, value: ""))
                    .ToList();

                if (tokens.Any(it => !it.isValid))
                {
                    serviceLevel = default;
                    return false;
                }

                serviceLevel = new ServiceLevel(string.Join("/", tokens.Select(it => it.value)));
                return true;
            }

            serviceLevel = default;
            return false;
        }

        public override string ToString() => _value;
    }

    public static class StringExtensions
    {
        public static ServiceLevel? ToServiceLevelOpt(this string? str)
        {
            if (str is null)
            {
                return null;
            }

            return ServiceLevel.TryParse(str, out var serviceLevel) ? serviceLevel : null;
        }
    }
}