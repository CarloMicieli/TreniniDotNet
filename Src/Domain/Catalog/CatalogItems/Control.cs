using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public enum Control
    {
        None,
        DccReady,
        Dcc,
        DccSound
    }

    public static class Controls
    {
        public static bool TryParse(string? v, out Control control)
        {
            if (v is null)
            {
                control = Control.None;
                return true;
            }

            if (string.IsNullOrWhiteSpace(v) == false && Enum.TryParse<Control>(v, true, out var c))
            {
                control = c;
                return true;
            }

            control = default;
            return false;
        }
    }
}