using System;

namespace TreniniDotNet.Common.Uuid
{
    public sealed class FakeGuidSource : IGuidSource
    {
        private readonly Guid _fake;

        private FakeGuidSource(Guid fake)
        {
            _fake = fake;
        }

        public Guid NewGuid() => _fake;

        public static IGuidSource NewSource(Guid id) => new FakeGuidSource(id);
    }
}