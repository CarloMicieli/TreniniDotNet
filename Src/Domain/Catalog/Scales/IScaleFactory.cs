using System;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public interface IScalesFactory
    {
        IScale NewScale(Guid id, string name, string slug, decimal? ratio, decimal? gauge, string? trackGauge, string? notes);
    }
}
