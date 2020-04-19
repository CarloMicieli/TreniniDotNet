﻿using System;
using System.Diagnostics.CodeAnalysis;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Shops
{
    public sealed class ShopInfo : IShopInfo
    {
        private ShopInfo(ShopId shopId, Slug slug, string name)
        {
            ShopId = shopId;
            Slug = slug;
            Name = name;
        }

        public ShopInfo(ShopId shopId, string name)
            : this(shopId, Slug.Of(name), name)
        {
        }

        public ShopId ShopId { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public static bool TryCreate(Guid? id, string? name, string? slug, [NotNullWhen(true)] out IShopInfo? info)
        {
            if (id.HasValue == false || name is null || slug is null)
            {
                info = default;
                return false;
            }

            info = ShopInfo.Create(id!.Value, name, slug);
            return true;
        }

        public static IShopInfo Create(Guid id, string name, string slug) =>
            new ShopInfo(new ShopId(id), Slug.Of(slug), name);
    }
}
