using System;
using TreniniDotNet.Common.Uuid;

namespace TreniniDotNet.Common.Domain
{
    public abstract class EntityFactory<TKey, TEntity> : IFactory<TKey, TEntity>
        where TKey : struct, IEquatable<TKey>
        where TEntity : Entity<TKey>
    {
        private IGuidSource GuidSource { get; }

        protected EntityFactory(IGuidSource guidSource)
        {
            GuidSource = guidSource ?? throw new ArgumentNullException(nameof(guidSource));
        }

        protected TKey NewId(Func<Guid, TKey> func) => func(GuidSource.NewGuid());
    }
}
