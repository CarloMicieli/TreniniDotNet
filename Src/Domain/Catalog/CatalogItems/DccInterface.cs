using System;

namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    // NMRA and NEM Connectors
    public enum DccInterface
    {
        None,
        Nem651,
        Nem652,
        Plux8,
        Plux16,
        Plux22,
        Next18,
        Mtc21
    }

    public static class DccInterfaces
    {
        public static bool TryParse(string? str, out DccInterface result)
        {
            if (str is null)
            {
                result = DccInterface.None;
                return true;
            }

            if (string.IsNullOrWhiteSpace(str) == false && Enum.TryParse<DccInterface>(str, true, out var dcc))
            {
                result = dcc;
                return true;
            }

            result = default;
            return false;
        }
    }
}