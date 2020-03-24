using System;

namespace TreniniDotNet.Common.Uuid
{
    public sealed class GuidSource : IGuidSource
    {
        public Guid NewGuid() => Guid.NewGuid();
    }
}