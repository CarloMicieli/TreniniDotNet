using System;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Domain.Collection.Shared
{
    public sealed class ScaleRef : IScaleRef, IEquatable<ScaleRef>
    {
        public ScaleRef(string name, Slug slug)
        {
            Name = name;
            Slug = slug;
        }

        public static IScaleRef FromScaleInfo(IScaleInfo info) =>
            new ScaleRef(info.ToLabel(), info.Slug);

        public string Name { get; }

        public Slug Slug { get; }

        public bool Equals(ScaleRef other)
        {
            throw new NotImplementedException();
        }
    }
}
