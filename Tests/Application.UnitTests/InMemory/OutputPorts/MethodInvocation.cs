using System;
using Xunit;

namespace TreniniDotNet.Application.InMemory.OutputPorts
{
    public interface IMethodInvocation
    {
        string Name { get; }

        bool IsInvoked { get; }

        string PrintContext();
    }

    public readonly struct MethodInvocation<T> : IMethodInvocation
    {
        private readonly string _methodName;
        private readonly bool _invoked;
        private readonly T _value;

        private readonly Func<string> _outputPortToString;

        public MethodInvocation(string methodName, bool invoked, T value)
            : this(methodName, invoked, value, () => string.Empty)
        {
        }

        public MethodInvocation(string methodName, bool invoked, T value, Func<string> outputPortToString)
        {
            _methodName = methodName;
            _invoked = invoked;
            _value = value;
            _outputPortToString = outputPortToString;
        }

        public static MethodInvocation<T> NotInvoked(string methodName) =>
            NotInvoked(methodName, () => string.Empty);

        public static MethodInvocation<T> NotInvoked(string methodName, Func<string> outputPortToString)
        {
            return new MethodInvocation<T>(methodName, false, default, outputPortToString);
        }

        public MethodInvocation<T> Invoked(T value)
        {
            return new MethodInvocation<T>(_methodName, true, value);
        }

        public T Argument => _value;

        public string Name => _methodName;

        public bool IsInvoked => _invoked;

        public void ShouldBeInvokedWithTheArgument(T expected)
        {
            AssertMethod.IsInvoked(this);
            Assert.Equal(expected, _value);
        }

        public void AssertArgumentIsNotNull()
        {
            AssertMethod.IsInvoked(this);
            Assert.NotNull(_value);
        }

        public override string ToString() =>
            $"{_methodName}(): invoked? " + (_invoked ? $"yes, with argument: '{_value}'" : "no");

        public string PrintContext() => _outputPortToString();
    }

    public readonly struct MethodInvocation<T1, T2> : IMethodInvocation
    {
        private readonly string _methodName;
        private readonly bool _invoked;
        private readonly T1 _value1;
        private readonly T2 _value2;

        private readonly Func<string> _outputPortToString;

        public MethodInvocation(string methodName, bool invoked, T1 value1, T2 value2)
            : this(methodName, invoked, value1, value2, () => string.Empty)
        {
        }

        public MethodInvocation(string methodName, bool invoked, T1 value1, T2 value2, Func<string> outputPortToString)
        {
            _methodName = methodName;
            _invoked = invoked;
            _value1 = value1;
            _value2 = value2;
            _outputPortToString = outputPortToString;
        }

        public static MethodInvocation<T1, T2> NotInvoked(string methodName) =>
            NotInvoked(methodName, () => string.Empty);

        public static MethodInvocation<T1, T2> NotInvoked(string methodName, Func<string> outputPortToString)
        {
            return new MethodInvocation<T1, T2>(methodName, false, default, default, outputPortToString);
        }

        public MethodInvocation<T1, T2> Invoked(T1 value1, T2 value2)
        {
            return new MethodInvocation<T1, T2>(_methodName, true, value1, value2);
        }

        public T1 Argument1 => _value1;
        public T2 Argument2 => _value2;

        public string Name => _methodName;

        public bool IsInvoked => _invoked;

        public void ShouldBeInvokedWithTheArguments(T1 expected1, T2 expected2)
        {
            AssertMethod.IsInvoked(this);

            Assert.Equal(expected1, _value1);
            Assert.Equal(expected2, _value2);
        }

        public void ArgumentsAreNotNull()
        {
            AssertMethod.IsInvoked(this);

            Assert.NotNull(_value1);
            Assert.NotNull(_value2);
        }

        public override string ToString() =>
            $"{_methodName}(): invoked? " + (_invoked ? $"yes, with arguments: '{_value1}', '{_value2}'" : "no");

        public string PrintContext() => _outputPortToString();
    }

    public readonly struct MethodInvocation<T1, T2, T3> : IMethodInvocation
    {
        private readonly string _methodName;
        private readonly bool _invoked;
        private readonly T1 _value1;
        private readonly T2 _value2;
        private readonly T3 _value3;

        private readonly Func<string> _outputPortToString;

        public MethodInvocation(string methodName, bool invoked, T1 value1, T2 value2, T3 value3)
            : this(methodName, invoked, value1, value2, value3, () => string.Empty)
        {
        }

        public MethodInvocation(string methodName, bool invoked, T1 value1, T2 value2, T3 value3, Func<string> outputPortToString)
        {
            _methodName = methodName;
            _invoked = invoked;
            _value1 = value1;
            _value2 = value2;
            _value3 = value3;
            _outputPortToString = outputPortToString;
        }

        public static MethodInvocation<T1, T2, T3> NotInvoked(string methodName) =>
            NotInvoked(methodName, () => string.Empty);

        public static MethodInvocation<T1, T2, T3> NotInvoked(string methodName, Func<string> outputPortToString)
        {
            return new MethodInvocation<T1, T2, T3>(methodName, false, default, default, default, outputPortToString);
        }

        public MethodInvocation<T1, T2, T3> Invoked(T1 value1, T2 value2, T3 value3)
        {
            return new MethodInvocation<T1, T2, T3>(_methodName, true, value1, value2, value3);
        }

        public T1 Argument1 => _value1;
        public T2 Argument2 => _value2;
        public T3 Argument3 => _value3;

        public string Name => _methodName;

        public bool IsInvoked => _invoked;

        public void ShouldBeInvokedWithTheArguments(T1 expected1, T2 expected2, T3 expected3)
        {
            AssertMethod.IsInvoked(this);

            Assert.Equal(expected1, _value1);
            Assert.Equal(expected2, _value2);
            Assert.Equal(expected3, _value3);
        }

        public void ArgumentsAreNotNull()
        {
            AssertMethod.IsInvoked(this);

            Assert.NotNull(_value1);
            Assert.NotNull(_value2);
            Assert.NotNull(_value3);
        }

        public override string ToString() =>
            $"{_methodName}(): invoked? " + (_invoked ? $"yes, with arguments: '{_value1}', '{_value2}', '{_value3}'" : "no");

        public string PrintContext() => _outputPortToString();
    }

    public static class AssertMethod
    {
        public static void IsInvoked(IMethodInvocation method)
        {
            if (method.IsInvoked == false)
            {
                string because = $"Expected {method.Name}() to be invoked, it was never called{method.PrintContext()}";
                Assert.True(method.IsInvoked, because);
            }
        }
    }
}
