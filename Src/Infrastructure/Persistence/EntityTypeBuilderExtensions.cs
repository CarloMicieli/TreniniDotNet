#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NodaTime;
using TreniniDotNet.Common.Domain;

namespace TreniniDotNet.Infrastructure.Persistence
{
    public static class EntityTypeBuilderExtensions
    {
        public static void AddAggregateRootProperties<TAggregate, TKey>(this EntityTypeBuilder<TAggregate> builder)
            where TKey : struct, IEquatable<TKey>
            where TAggregate : AggregateRoot<TKey>
        {
            builder.Property(x => x.CreatedDate)
                .HasColumnName("created")
                .HasConversion(
                    instant => instant.ToDateTimeUtc(),
                    date => InstantFromDateTime(date));

            builder.Property(x => x.ModifiedDate)
                .HasColumnName("last_modified")
                .HasConversion(
                    instant => instant.Value.ToDateTimeUtc(),
                    date => InstantFromDateTime(date));

            builder.Property(x => x.Version)
                .HasColumnName("version");
        }

        private static Instant InstantFromDateTime(DateTime dateTime) =>
            Instant.FromDateTimeUtc(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
    }
}
