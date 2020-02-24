using FluentValidation.Results;
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

        protected OutputPortTestHelper()
        {
            this.ErrorMethod = MethodInvocation<string>.NotInvoked(nameof(Error));
            this.InvalidRequestMethod = MethodInvocation<IList<ValidationFailure>>.NotInvoked(nameof(InvalidRequest));
            this.StandardMethod = MethodInvocation<TUseCaseOutput>.NotInvoked(nameof(Standard));
        }

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
            ErrorMethod.InvokedWithArgument(expectedMessage);
        }

        public void ShouldHaveStandardOutputEqualTo(TUseCaseOutput expectedOutput)
        {
            StandardMethod.InvokedWithArgument(expectedOutput);
        }

        public void ShouldHaveStandardOutput()
        {
            StandardMethod.ArgumentIsNotNull();
        }

        public void ShouldHaveValidationErrors()
        {
            var validationFailures = InvalidRequestMethod.Argument;
            Assert.NotEmpty(validationFailures);
        }

        public void ShouldHaveValidationErrorFor(string name)
        {
            bool hasError = InvalidRequestMethod.Argument.Any(vf => vf.PropertyName == name);
            Assert.True(hasError, $"Expected validation error(s) for {name}, none was found");
        }

        public TUseCaseOutput UseCaseOutput => StandardMethod.Argument;
    }
}
