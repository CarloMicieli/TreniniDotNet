using System;
using System.Runtime.CompilerServices;
using NodaTime;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.Railways
{
    public sealed class Railway : ModifiableEntity, IEquatable<Railway>, IRailway
    {
        internal Railway(
            RailwayId id,
            Slug slug, string name,
            string? companyName,
            Country? country,
            PeriodOfActivity periodOfActivity,
            RailwayLength? railwayLength,
            RailwayGauge? gauge,
            Uri? websiteUrl,
            string? headquarters,
            Instant created,
            Instant? modified,
            int version)
            : base(created, modified, version)
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
        }

        #region [ Properties ]
        public RailwayId RailwayId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public string? CompanyName { get; }

        public Country? Country { get; }

        public PeriodOfActivity PeriodOfActivity { get; }

        public RailwayGauge? TrackGauge { get; } = null!;

        public RailwayLength? TotalLength { get; }

        public Uri? WebsiteUrl { get; }

        public string? Headquarters { get; }
        #endregion

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is Railway that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(Railway that) => AreEquals(this, that);

        private static bool AreEquals(Railway left, Railway right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }
            return left.Name == right.Name;
        }

        #endregion

        #region [ Standard methods overrides ]
        public override int GetHashCode() => HashCode.Combine(Name);

        public override string ToString() => $"{Name}";

        #endregion

        public IRailwayInfo ToRailwayInfo() => this;
    }
}
