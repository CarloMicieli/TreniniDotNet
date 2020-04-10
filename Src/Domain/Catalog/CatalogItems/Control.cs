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
        //TODO: test me
        public static Control? ValueFor(string? str)
        {
            if (string.IsNullOrWhiteSpace(str) == false &&
                Enum.TryParse<Control>(str, true, out var c))
            {
                return c;
            }

            return null;
        }

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