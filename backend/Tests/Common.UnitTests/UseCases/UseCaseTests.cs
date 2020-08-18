using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs.Validation;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;
using Xunit;

namespace TreniniDotNet.Common.UseCases
{
    public class UseCaseTests
    {
        private UseCaseOutputPort OutputPort { get; }
        private CalculatorUseCase UseCase { get; }

        public UseCaseTests()
        {
            OutputPort = new UseCaseOutputPort();
            UseCase = new CalculatorUseCase(new UseCaseInputValidator(), OutputPort);
        }

        [Fact]
        public async Task UseCase_ShouldOutputError_WhenInputIsNull()
        {
            await UseCase.Execute(null);

            OutputPort.ErrorMessage.Should().Be("The use case input is null");
        }

        [Fact]
        public async Task UseCase_ShouldOutputInvalidRequest_WhenInputFailsValidation()
        {
            await UseCase.Execute(new UseCaseInput
            {
                Value1 = -21,
                Value2 = 21
            });

            OutputPort.ValidationErrors
                .Should().Contain(new ValidationError("Value1", "Negative"));
        }

        [Fact]
        public async Task UseCase_ShouldProduceStandardOutput()
        {
            await UseCase.Execute(new UseCaseInput
            {
                Value1 = 21,
                Value2 = 21
            });

            OutputPort.Output.Result.Should().Be(42);
        }
    }

    internal class UseCaseInput : IUseCaseInput
    {
        public int Value1 { set; get; }
        public int Value2 { set; get; }
    }

    internal class UseCaseOutput : IUseCaseOutput
    {
        public int Result { set; get; }
    }

    internal class UseCaseOutputPort : IStandardOutputPort<UseCaseOutput>
    {
        public UseCaseOutput Output { get; set; }
        public IEnumerable<ValidationError> ValidationErrors { get; set; }
        public string ErrorMessage { get; set; }

        public void Standard(UseCaseOutput output)
        {
            Output = output;
        }

        public void InvalidRequest(IEnumerable<ValidationError> validationErrors)
        {
            ValidationErrors = validationErrors;
        }

        public void Error(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }

    internal class UseCaseInputValidator : IUseCaseInputValidator<UseCaseInput>
    {
        public IReadOnlyList<ValidationError> ValidateInput(UseCaseInput input)
        {
            var list = new List<ValidationError>();

            if (input.Value1 < 0)
            {
                list.Add(new ValidationError(nameof(input.Value1), "Negative"));
            }

            if (input.Value2 < 0)
            {
                list.Add(new ValidationError(nameof(input.Value2), "Negative"));
            }

            return list;
        }
    }

    internal class CalculatorUseCase : AbstractUseCase<UseCaseInput, UseCaseOutput, UseCaseOutputPort>
    {
        public CalculatorUseCase(IUseCaseInputValidator<UseCaseInput> inputValidator, UseCaseOutputPort outputPort)
            : base(inputValidator, outputPort)
        {
        }

        protected override Task Handle(UseCaseInput input)
        {
            OutputPort.Standard(new UseCaseOutput
            {
                Result = input.Value1 + input.Value2
            });

            return Task.CompletedTask;
        }
    }
}
