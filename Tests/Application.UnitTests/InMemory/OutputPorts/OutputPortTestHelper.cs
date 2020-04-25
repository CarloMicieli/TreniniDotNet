using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries;
using Xunit;

namespace TreniniDotNet.Application.InMemory.OutputPorts
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
            if (InvalidRequestMethod.Argument is null)
            {
                Assert.True(false, "No validation error has been found");
            }
            else
            {
                var validationFailures = InvalidRequestMethod.Argument;
                Assert.NotEmpty(validationFailures);
            }
        }

        public void ShouldHaveNoValidationError()
        {
            var validationFailures = InvalidRequestMethod.Argument;
            Assert.Null(validationFailures);
        }

        public void ShouldHaveValidationErrorFor(string name)
        {
            bool hasError = InvalidRequestMethod.Argument.Any(vf => vf.PropertyName == name);
            Assert.True(hasError, $"Expected validation error(s) for {name}, none was found");
        }

        public TUseCaseOutput UseCaseOutput => StandardMethod.Argument;

        public virtual IEnumerable<IMethodInvocation> Methods => CommonMethods;

        public override string ToString() =>
            $" and this is the output port status:{Environment.NewLine}{string.Join(Environment.NewLine, Methods.OrderBy(it => it.Name).Select(it => "   - " + it.ToString()))}";
    }
}
