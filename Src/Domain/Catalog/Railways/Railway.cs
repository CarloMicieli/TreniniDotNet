﻿using TreniniDotNet.Common;
using System;
using System.Globalization;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class Railway : IEquatable<Railway>, IRailway
    {
        private readonly RailwayId _id;
        private readonly Slug _slug;
        private readonly string _name;
        private readonly string? _companyName;
        private readonly RegionInfo? _country;
        private readonly PeriodOfActivity _periodOfActivity;
        private readonly DateTime? _createdAt;
        private readonly int? _version;

        [Obsolete]
        public Railway(string name, string? companyName, string? country, RailwayStatus? rs)
            : this(RailwayId.NewId(), Slug.Empty, name, companyName, country, null, null, rs, DateTime.UtcNow, 1)
        {
        }

        [Obsolete]
        public Railway(string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs)
            : this(RailwayId.NewId(), Slug.Empty, name, companyName, country, operatingSince, operatingUntil, rs, DateTime.UtcNow, 1)
        {
        }

        [Obsolete]
        public Railway(RailwayId id, Slug slug, string name, string? companyName, string? country, DateTime? operatingSince, DateTime? operatingUntil, RailwayStatus? rs, DateTime? createdAt, int version)
        {
            ValidateCountryCode(country);
            ValidateName(name);
            ValidateStatusValues(operatingSince, operatingUntil, rs);

            _id = id;
            _slug = slug.OrNewIfEmpty(() => Slug.Of(name));
            _name = name;
            _companyName = companyName;
            _country = new RegionInfo(country);
            _periodOfActivity = new PeriodOfActivity(operatingSince, operatingUntil, rs ?? RailwayStatus.Active);
            _createdAt = createdAt;
            _version = version;
        }

        internal Railway(RailwayId id, Slug slug, string name, string? companyName, RegionInfo? country, PeriodOfActivity periodOfActivity, DateTime? createdAt, int version)
        {
            _id = id;
            _slug = slug.OrNewIfEmpty(() => Slug.Of(name));
            _name = name;
            _companyName = companyName;
            _country = country;
            _periodOfActivity = periodOfActivity;
            _createdAt = createdAt;
            _version = version;
        }

        #region [ Properties ]
        public RailwayId RailwayId => _id;

        public Slug Slug => _slug;

        public string Name => _name;

        public string? CompanyName => _companyName;

        public string? Country => _country?.TwoLetterISORegionName;

        public RailwayStatus? Status => _periodOfActivity.RailwayStatus;

        public DateTime? OperatingUntil => _periodOfActivity.OperatingUntil;

        public DateTime? OperatingSince => _periodOfActivity.OperatingSince;

        public DateTime? CreatedAt => _createdAt;

        public int? Version => _version;
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

        public IRailwayInfo ToRailwayInfo()
        {
            return this;
        }

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
