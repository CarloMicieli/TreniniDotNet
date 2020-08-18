using System;
using NodaTime;
using TreniniDotNet.Common.Uuid;

namespace TreniniDotNet.Common.Domain
{
    public abstract class AggregateRootFactory<TKey, TAggregate> : IFactory<TKey, TAggregate>
        where TKey : struct, IEquatable<TKey>
        where TAggregate : AggregateRoot<TKey>
    {
        private IClock Clock { get; }
        private IGuidSource GuidSource { get; }

        protected AggregateRootFactory(IClock clock, IGuidSource guidSource)
        {
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
            GuidSource = guidSource ?? throw new ArgumentNullException(nameof(guidSource));
        }

        protected Instant GetCurrentInstant() => Clock.GetCurrentInstant();

        protected TKey NewId(Func<Guid, TKey> func) => func(GuidSource.NewGuid());
    }
}
