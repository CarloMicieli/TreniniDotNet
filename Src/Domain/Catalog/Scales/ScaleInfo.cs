﻿using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Domain.Catalog.Scales
{
    public sealed class ScaleInfo : IScaleInfo, IEquatable<ScaleInfo>
    {
        public ScaleInfo(Guid scaleId, string slug, string name, decimal ratio)
            : this(new ScaleId(scaleId), Slug.Of(slug), name, Ratio.Of(ratio))
        {
        }

        public ScaleInfo(ScaleId scaleId, Slug slug, string name, Ratio ratio)
        {
            Id = scaleId;
            Slug = slug;
            Name = name;
            Ratio = ratio;
        }

        public ScaleId Id { get; }

        public Slug Slug { get; }

        public string Name { get; }

        public Ratio Ratio { get; }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(ScaleInfo left, ScaleInfo right) => AreEquals(left, right);

        public static bool operator !=(ScaleInfo left, ScaleInfo right) => !AreEquals(left, right);

        public override bool Equals(object obj)
        {
            if (obj is ScaleInfo that)
            {
                return AreEquals(this, that);
            }

            return false;
        }

        public bool Equals(ScaleInfo other) =>
            AreEquals(this, other);

        private static bool AreEquals(ScaleInfo left, ScaleInfo right) =>
            left.Id.Equals(right.Id);

        public IScaleInfo ToScaleInfo() => this;
    }
}