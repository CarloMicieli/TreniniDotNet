﻿using TreniniDotNet.Common;
using System;
using System.Globalization;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    /// <summary>
    /// It represents a operator of the rail transport.
    /// </summary>
    public sealed class Railway : IEquatable<Railway>, IRailway
    {
        private readonly GuidId _id;
        private readonly Slug _slug;
        private readonly string _name;
        private readonly string? _companyName;
        private readonly string? _country;
        private readonly RailwayStatus? _rs;
        private readonly DateTime? _operatingSince;
        private readonly DateTime? _operatingUntil;

        public Railway(string name, string? companyName, string? country, RailwayStatus? rs)
            : this(GuidId.NewId(), Slug.Empty, name, companyName, country, null, null, rs)
        {
        }

        public Railway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
            : this(GuidId.NewId(), Slug.Empty, name, companyName, country, operatingSince, operatingUntil, rs)
        {
        }

        public Railway(GuidId id, Slug slug, string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
        {
            ValidateCountryCode(country);
            ValidateName(name);
            ValidateStatusValues(operatingSince, operatingUntil, rs);

            _id = id;
            _slug = slug.OrNewIfEmpty(() => Slug.Of(name));
            _name = name;
            _companyName = companyName;
            _country = country;
            _rs = rs;
            _operatingSince = operatingSince;
            _operatingUntil = operatingUntil;
        }

        #region [ Properties ]
        public GuidId RailwayId => _id;

        public Slug Slug => _slug;

        public string Name => _name;

        public string? CompanyName => _companyName;

        public string? Country => _country;

        public RailwayStatus? Status => _rs;

        public DateTime? OperatingUntil => _operatingUntil;

        public DateTime? OperatingSince => _operatingSince;
        #endregion

        #region [ Equality ]
        public static bool operator ==(Railway left, Railway right)
        {
            return AreEquals(left, right);
        }

        public static bool operator !=(Railway left, Railway right)
        {
            return !AreEquals(left, right);
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            
            if (obj is Railway that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Railway that)
        {
            return AreEquals(this, that);
        }

        private static bool AreEquals(Railway left, Railway right)
        {
            return left.Name == right.Name;
        }
        #endregion

        #region [ Standard methods overrides ]
        public override int GetHashCode()
        {
            return this._name.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return $"{_name}";
        }
        #endregion

        private static void ValidateCountryCode(string? countryCode)
        {
            if (countryCode != null)
            {
                var _ = new RegionInfo(countryCode);
            }
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(ExceptionMessages.InvalidRailwayName);
            }
        }

        private void ValidateStatusValues(DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
        {
            if (operatingSince.HasValue && operatingUntil.HasValue)
            {
                if (operatingSince.Value.CompareTo(operatingUntil.Value) > 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRailwaySinceLaterThanOperatingUntil);
                }
            }

            if (operatingUntil.HasValue && rs.HasValue && rs == RailwayStatus.Active)
            {
                throw new ArgumentException(ExceptionMessages.InvalidRailwayOperatingUntilForInactiveRailway);
            }
        }
    }
}
