using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentValidation.Results;
using TreniniDotNet.Common.UseCases.Interfaces.Output;

namespace TreniniDotNet.TestHelpers.InMemory.OutputPorts
{
    public abstract class OutputPortTestHelper<TUseCaseOutput> : IOutputPortStandard<TUseCaseOutput>
        where TUseCaseOutput : IUseCaseOutput
    {
        private MethodInvocation<string> ErrorMethod { set; get; }
        private MethodInvocation<IList<ValidationFailure>> InvalidRequestMethod { set; get; }
        private MethodInvocation<TUseCaseOutput> StandardMethod { set; get; }

        private IEnumerable<IMethodInvocation> CommonMethods => new List<IMethodInvocation>
        {
            ErrorMethod, InvalidRequestMethod, StandardMethod
        };

        protected OutputPortTestHelper()
        {
            this.ErrorMethod = MethodInvocation<string>.NotInvoked(nameof(Error));
            this.InvalidRequestMethod = MethodInvocation<IList<ValidationFailure>>.NotInvoked(nameof(InvalidRequest));
            this.StandardMethod = MethodInvocation<TUseCaseOutput>.NotInvoked(nameof(Standard));
        }

        protected MethodInvocation<TValue> NewMethod<TValue>(string methodName) =>
            MethodInvocation<TValue>.NotInvoked(methodName, () => this.ToString());

        protected MethodInvocation<TValue1, TValue2> NewMethod<TValue1, TValue2>(string methodName) =>
            MethodInvocation<TValue1, TValue2>.NotInvoked(methodName, () => this.ToString());

        protected MethodInvocation<TValue1, TValue2, TValue3> NewMethod<TValue1, TValue2, TValue3>(string methodName) =>
            MethodInvocation<TValue1, TValue2, TValue3>.NotInvoked(methodName, () => this.ToString());

        public void Error(string message)
        {
            this.ErrorMethod = this.ErrorMethod.Invoked(message);
        }

        public void InvalidRequest(IList<ValidationFailure> failures)
        {
            this.InvalidRequestMethod = this.InvalidRequestMethod.Invoked(failures);
        }

        public void Standard(TUseCaseOutput output)
        {
            this.StandardMethod = this.StandardMethod.Invoked(output);
        }

        public void ShouldHaveErrorMessage(string expectedMessage)
        {
            ErrorMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public void ShouldHaveStandardOutputEqualTo(TUseCaseOutput expectedOutput)
        {
            StandardMethod.ShouldBeInvokedWithTheArgument(expectedOutput);
        }

        public void ShouldHaveStandardOutput()
        {
            StandardMethod.AssertArgumentIsNotNull();
        }

        public void ShouldHaveValidationErrors()
        {
            InvalidRequestMethod.Argument.Should().NotBeNull("No validation error has been found");
            InvalidRequestMethod.Argument.Should().NotBeEmpty();
        }

        public void ShouldHaveNoValidationError()
        {
            var validationFailures = InvalidRequestMethod.Argument;
            validationFailures.Should().BeNull();
        }

        public void ShouldHaveValidationErrorFor(string name)
        {
            var hasError = InvalidRequestMethod.Argument.Any(vf => vf.PropertyName == name);
            hasError.Should().BeTrue($"Expected validation error(s) for {name}, none was found");
        }

        public TUseCaseOutput UseCaseOutput => StandardMethod.Argument;

        public virtual IEnumerable<IMethodInvocation> Methods => CommonMethods;

        public override string ToString() =>
            $" and this is the output port status:{Environment.NewLine}{string.Join(Environment.NewLine, Methods.OrderBy(it => it.Name).Select(it => "   - " + it.ToString()))}";
    }
}
