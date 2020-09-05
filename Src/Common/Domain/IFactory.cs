using System;

namespace TreniniDotNet.Common.Domain
{
    public interface IFactory<TKey, TEntity>
        where TKey : struct, IEquatable<TKey>
        where TEntity : Entity<TKey>
    {
    }
}
