using System;

namespace TreniniDotNet.Common.Uuid
{
    public interface IGuidSource
    {
        Guid NewGuid();
    }
}