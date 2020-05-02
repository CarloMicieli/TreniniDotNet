using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class Epoch : IEquatable<Epoch>
    {
        private readonly string _value1;
        private readonly string? _value2;

        private static IReadOnlyDictionary<string, Epoch> _cachedValues;

        static Epoch()
        {
            _cachedValues = new Dictionary<string, Epoch>
            {
                {Epoch.I.ToString().ToUpper(), Epoch.I},
                {Epoch.II.ToString().ToUpper(), Epoch.II},
                {Epoch.III.ToString().ToUpper(), Epoch.III},
                {Epoch.IIIa.ToString().ToUpper(), Epoch.IIIa},
                {Epoch.IIIb.ToString().ToUpper(), Epoch.IIIb},
                {Epoch.IV.ToString().ToUpper(), Epoch.IV},
                {Epoch.V.ToString().ToUpper(), Epoch.V},
                {Epoch.VI.ToString().ToUpper(), Epoch.VI},
            };
        }

        private Epoch(string epoch1, string? epoch2 = null)
        {
            _value1 = epoch1;
            _value2 = epoch2;
        }

        public static readonly Epoch I = new Epoch("I");

        public static readonly Epoch II = new Epoch("II");

        public static readonly Epoch III = new Epoch("III");

        public static readonly Epoch IIIa = new Epoch("IIIa");

        public static readonly Epoch IIIb = new Epoch("IIIb");

        public static readonly Epoch IV = new Epoch("IV");

        public static readonly Epoch V = new Epoch("V");

        public static readonly Epoch VI = new Epoch("VI");

        public static Epoch Parse(string str)
        {
            if (TryParse(str, out var epoch))
            {
                return epoch;
            }

            throw new ArgumentOutOfRangeException(nameof(str), "The value is not a valid Epoch");
        }

        public static bool TryParse(string str, [NotNullWhen(true)] out Epoch? epoch)
        {
            if (str.IndexOf('/') != -1)
            {
                var tokens = str.Split('/');
                if (tokens.Length == 2)
                {
                    var ret1 = ParseOneEpoch(tokens[0], out var ep1);
                    var ret2 = ParseOneEpoch(tokens[1], out var ep2);

                    if (ret1 && ret2)
                    {
                        epoch = new Epoch(ep1!._value1, ep2!._value1);
                        return true;
                    }

                    epoch = default;
                    return false;
                }
            }

            if (ParseOneEpoch(str, out var e))
            {
                epoch = e;
                return true;
            }

            epoch = default;
            return false;
        }

        private static bool ParseOneEpoch(string s, [NotNullWhen(true)] out Epoch? epp)
        {
            if (s.IndexOf('/') == -1 &&
                _cachedValues.TryGetValue(s.ToUpper(), out var ep))
            {
                epp = ep;
                return true;
            }

            epp = default;
            return false;
        }

        public override string ToString() =>
            _value2 is null ? _value1 : $"{_value1}/{_value2}";

        public override int GetHashCode() => HashCode.Combine(_value1, _value2);

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Epoch other && Equals(other);
        }

        public bool Equals(Epoch? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value1 == other._value1 && _value2 == other._value2;
        }

        public static bool operator ==(Epoch left, Epoch right) => left.Equals(right);

        public static bool operator !=(Epoch left, Epoch right) => !left.Equals(right);

        #endregion
    }
}
