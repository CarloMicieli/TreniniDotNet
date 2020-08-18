using System;
using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Domain
{
    public class EntityTests
    {
        [Fact]
        public void Entity_ShouldCompareTwoValues()
        {
            var id = Guid.NewGuid();

            var e1 = new MyEntity(id, "value1");
            var e2 = new MyEntity(id, "value2");

            (e1 == e2).Should().BeTrue();
            e1.Equals(e2).Should().BeTrue();
            (e1 == null).Should().BeFalse();
            e1.Equals(null).Should().BeFalse();
        }

        [Fact]
        public void Entity_ShouldGetHashCode()
        {
            var id = Guid.NewGuid();

            var e1 = new MyEntity(id, "value1");
            var e2 = new MyEntity(id, "value2");

            e1.GetHashCode().Should().Be(e2.GetHashCode());
        }
    }

    class MyEntity : Entity<Guid>
    {
        public string Value { get; }

        public MyEntity(Guid id, string value)
            : base(id)
        {
            Value = value;
        }
    }
}
