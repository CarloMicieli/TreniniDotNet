using System;

namespace TreniniDotNet.Common.Uuid.Testing
{
    public sealed class FakeGuidSource : IGuidSource
    {
        public Guid FakeGuid { set; get; }

        private FakeGuidSource(Guid fake)
        {
            FakeGuid = fake;
        }

        public Guid NewGuid() => FakeGuid;

        public static IGuidSource NewSource(Guid id) => new FakeGuidSource(id);
    }
}