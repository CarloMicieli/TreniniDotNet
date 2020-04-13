using NodaTime;
using System;
using TreniniDotNet.Common.Uuid;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public sealed class CollectionsFactory : ICollectionsFactory
    {
        private readonly IClock _clock;
        private readonly IGuidSource _guidSource;

        public CollectionsFactory(IClock clock, IGuidSource guidSource)
        {
            _clock = clock ??
                throw new ArgumentNullException(nameof(clock));

            _guidSource = guidSource ??
                throw new ArgumentNullException(nameof(guidSource));
        }
    }
}
