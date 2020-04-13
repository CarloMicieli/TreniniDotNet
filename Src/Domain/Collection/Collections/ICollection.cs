﻿using System.Collections.Immutable;
using TreniniDotNet.Common.Entities;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Domain.Collection.Collections
{
    public interface ICollection : IModifiableEntity
    {
        CollectionId CollectionId { get; }

        string Owner { get; }

        string? Notes { get; }

        IImmutableList<ICollectionItem> Items { get; }
    }
}