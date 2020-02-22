using TreniniDotNet.Common;
using System;
using Xunit;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Railways
{
    public class RailwayTests
    {
        [Fact]
        public void ItShouldCreateNewRailways()
        {
            var dieBahn = new Railway("DB", null, "de", RailwayStatus.Active);
            Assert.Equal("DB", dieBahn.ToString());
        }

        [Fact]
        public void ItShouldSetTheRailwayIdWhenOneIsNotProvided()
        {
            var dieBahn = new Railway("die Bahn", null, "de", RailwayStatus.Active);
            Assert.Equal(Slug.Of("die-bahn"), dieBahn.Slug);
        }

        [Fact]
        public void ItShouldCheckRailwaysEquality()
        {
            var db1 = DieBahn();
            var db2 = DieBahn();

            Assert.True(db1 == db2);
            Assert.True(db1.Equals(db2));
        }

        [Fact]
        public void ItShouldCheckRailwaysInequality()
        {
            var db = DieBahn();
            var fs = Fs();

            Assert.True(db != fs);
            Assert.False(db.Equals(fs));
        }

        [Fact]
        public void ItShouldThrowAnExceptionWhenTheCountyCodeIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Railway("die Bahn", null, "sq", null));
        }

        [Fact]
        public void ItShouldThrowAnExceptionWhenTheNameIsABlankString()
        {
            Assert.Throws<ArgumentException>(() => new Railway(GuidId.NewId(), Slug.Of("db"), "  ", null, "de", null, null, null));
        }

        [Fact]
        public void ItShouldThrowAnExceptionWhenOperatingSinceIsLaterThanOperatingUntil()
        {
            DateTime since = new DateTime(2000, 1, 1);
            DateTime until = since.AddDays(-5);

            Assert.Throws<ArgumentException>(() => new Railway("Die Bahn", null, "DE", since, until, null));
        }

        [Fact]
        public void ItShouldThrowAnExceptionWhenOperatingUntilIsNotNullAndStatusIsActive()
        {
            DateTime since = new DateTime(2000, 1, 1);
            DateTime until = since.AddDays(5);

            Assert.Throws<ArgumentException>((Func<object>)(() => new Railway("Die Bahn", null, "DE", since, until, RailwayStatus.Active)));
        }

        [Fact]
        public void ItShouldReturnRailwayProperties()
        {
            var name = "Die Bahn";
            var companyName = "Die Bahn AG";
            var country = "DE";
            var status = RailwayStatus.Inactive;
            var since = new DateTime(1945, 1, 1);
            var until = DateTime.MaxValue;

            var dieBahn = new Railway(name, companyName, country, since, until, status);

            Assert.Equal(name, dieBahn.Name);
            Assert.Equal(companyName, dieBahn.CompanyName);
            Assert.Equal(country, dieBahn.Country);
            Assert.Equal(status, dieBahn.Status);
            Assert.Equal(since, dieBahn.OperatingSince);
            Assert.Equal(until, dieBahn.OperatingUntil);
        }

        private static Railway DieBahn()
        {
            return new Railway("die Bahn", null, "DE", RailwayStatus.Active);
        }

        private static Railway Fs()
        {
            return new Railway("FS", null, "IT", RailwayStatus.Active);
        }
    }
}
