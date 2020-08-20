using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;

namespace TreniniDotNet.Infrastructure.Persistence.Collecting
{
    public sealed class ShopFavourite
    {
        public Owner Owner { get; set; }
        public ShopId ShopId { get; set; }
        public Shop Shop { get; set; } = null!;
    }
}
