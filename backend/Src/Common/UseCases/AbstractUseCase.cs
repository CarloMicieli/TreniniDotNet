using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs;
using TreniniDotNet.Common.UseCases.Boundaries.Outputs.Ports;

namespace TreniniDotNet.Common.UseCases
{
    public abstract class AbstractUseCase<TUseCaseInput, TUseCaseOutput, TUseCaseOutputPort>
        : IUseCase<TUseCaseInput>
        where TUseCaseInput : IUseCaseInput
        where TUseCaseOutput : IUseCaseOutput
        where TUseCaseOutputPort : IStandardOutputPort<TUseCaseOutput>
    {
        protected AbstractUseCase(IUseCaseInputValidator<TUseCaseInput> inputValidator, TUseCaseOutputPort outputPort)
        {
            InputValidator = inputValidator ?? throw new ArgumentNullException(nameof(inputValidator));
            OutputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        public Task Execute(TUseCaseInput input)
        {
            if (input is null)
            {
                OutputPort.Error("The use case input is null");
                return Task.CompletedTask;
            }

            var validationErrors = InputValidator.ValidateInput(input);
            if (validationErrors.Any())
            {
                OutputPort.InvalidRequest(validationErrors);
                return Task.CompletedTask;
            }

            return Handle(input);
        }

        private IUseCaseInputValidator<TUseCaseInput> InputValidator { get; }

        protected TUseCaseOutputPort OutputPort { get; }

        protected abstract Task Handle(TUseCaseInput input);
    }
}
