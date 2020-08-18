using System;
using NodaTime;
using NodaTime.Testing;
using TreniniDotNet.Common.Uuid;
using TreniniDotNet.Common.Uuid.Testing;

namespace TreniniDotNet.TestHelpers.InMemory.Domain
{
    public sealed class Factories<TFactory>
    {
        private Instant? _instant;
        private Guid? _guid;
        private readonly Func<IClock, IGuidSource, TFactory> _buildFunc;

        private Factories(Func<IClock, IGuidSource, TFactory> buildFunc)
        {
            _buildFunc = buildFunc;
        }

        public static Factories<T> New<T>(Func<IClock, IGuidSource, T> buildFunc) =>
            new Factories<T>(buildFunc);

        public Factories<TFactory> CurrentInstant(Instant instant)
        {
            _instant = instant;
            return this;
        }

        public Factories<TFactory> Id(Guid guid)
        {
            _guid = guid;
            return this;
        }

        public TFactory Build()
        {
            var guidSource = FakeGuidSource.NewSource(_guid ?? Guid.NewGuid());
            var clock = new FakeClock(_instant ?? Instant.FromUtc(1988, 11, 25, 10, 30));

            return _buildFunc(clock, guidSource);
        }
    }
}
