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

        public void InvokedWithArgument(T expected)
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
}
