using TreniniDotNet.Common;
using System;
using System.Globalization;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using NodaTime;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class Railway : IEquatable<Railway>, IRailway
    {
        internal Railway(
            RailwayId id,
            Slug slug, string name,
            string? companyName,
            Country country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters,
            Instant? lastModified, int version)
        {
            RailwayId = id;
            Slug = slug;
            Name = name;
            CompanyName = companyName;
            Country = country;
            PeriodOfActivity = periodOfActivity;
            TrackGauge = gauge;
            TotalLength = railwayLength;
            WebsiteUrl = websiteUrl;
            Headquarters = headquarters;
            LastModifiedAt = lastModified;
            Version = version;
        }

        #region [ Properties ]
        public RailwayId RailwayId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public string? CompanyName { get; }

        public Country Country { get; }

        public PeriodOfActivity PeriodOfActivity { get; }

        public RailwayGauge? TrackGauge { get; }

        public RailwayLength? TotalLength { get; }

        public Uri? WebsiteUrl { get; }

        public string? Headquarters { get; }

        public Instant? LastModifiedAt { get; }

        public int? Version { get; }
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
            return Name.GetHashCode(StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Name}";
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
