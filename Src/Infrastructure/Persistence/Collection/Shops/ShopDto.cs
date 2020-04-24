using System;

namespace TreniniDotNet.Infrastructure.Persistence.Collection.Shops
{
#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable CA1812 // Avoid uninstantiated internal classes
#pragma warning disable IDE1006 // Naming Styles
    internal sealed class ShopDto : ShopInfoDto
    {
        public string? website_url { get; set; }
        public string? phone_number { get; set; }
        public string? mail_address { get; set; }
        public string? address_line1 { get; set; }
        public string? address_line2 { get; set; }
        public string? address_city { get; set; }
        public string? address_region { get; set; }
        public string? address_postal_code { get; set; }
        public string? address_country { get; set; }
    }
#pragma warning restore CA1707 // Identifiers should not contain underscores
#pragma warning restore CA1812 // Avoid uninstantiated internal classes
#pragma warning restore IDE1006 // Naming Styles
}
