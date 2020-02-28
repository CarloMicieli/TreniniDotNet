using Xunit;

namespace TreniniDotNet.Application.InMemory.OutputPorts
{
    public readonly struct MethodInvocation<T>
    {
        private readonly string _methodName;
        private readonly bool _invoked;
        private readonly T _value;

        public MethodInvocation(string methodName, bool invoked, T value)
        {
            _methodName = methodName;
            _invoked = invoked;
            _value = value;
        }

        public static MethodInvocation<T> NotInvoked(string methodName)
        {
            return new MethodInvocation<T>(methodName, false, default);
        }

        public MethodInvocation<T> Invoked(T value)
        {
            return new MethodInvocation<T>(_methodName, true, value);
        }

        public T Argument => _value;

        public void ShouldBeInvokedWithTheArgument(T expected)
        {
            Assert.True(_invoked, $"Expected {_methodName} to be invoked, it was never called");
            Assert.Equal(expected, _value);
        }

        public void ArgumentIsNotNull()
        {
            Assert.True(_invoked, $"Expected {_methodName} to be invoked, it was never called");
            Assert.NotNull(_value);
        }
    }

    public readonly struct MethodInvocation<T1, T2>
    {
        private readonly string _methodName;
        private readonly bool _invoked;
        private readonly T1 _value1;
        private readonly T2 _value2;

        public MethodInvocation(string methodName, bool invoked, T1 value1, T2 value2)
        {
            _methodName = methodName;
            _invoked = invoked;
            _value1 = value1;
            _value2 = value2;
        }

        public static MethodInvocation<T1, T2> NotInvoked(string methodName)
        {
            return new MethodInvocation<T1, T2>(methodName, false, default, default);
        }

        public MethodInvocation<T1, T2> Invoked(T1 value1, T2 value2)
        {
            return new MethodInvocation<T1, T2>(_methodName, true, value1, value2);
        }

        public T1 Argument1 => _value1;
        public T2 Argument2 => _value2;

        public void ShouldBeInvokedWithTheArguments(T1 expected1, T2 expected2)
        {
            Assert.True(_invoked, $"Expected {_methodName} to be invoked, it was never called");
            Assert.Equal(expected1, _value1);
            Assert.Equal(expected2, _value2);
        }

        public void ArgumentsAreNotNull()
        {
            Assert.True(_invoked, $"Expected {_methodName} to be invoked, it was never called");
            Assert.NotNull(_value1);
            Assert.NotNull(_value2);
        }
    }
}
