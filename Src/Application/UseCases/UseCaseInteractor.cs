using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public sealed class UseCaseInteractor<TInput, TOutputPort>
        where TInput : IUseCaseInput
        where TOutputPort : IOutputPortErrors
    {
        private readonly IUseCaseInputValidator<TInput> _validator;
        private readonly TOutputPort _output;
        private readonly IUseCase<TInput> _useCase;

        public UseCaseInteractor(
            IUseCaseInputValidator<TInput> validator,
            IUseCase<TInput> useCase,
            TOutputPort output)
        {
            this._validator = validator ??
                throw new ArgumentNullException(nameof(validator));

            this._output = output ??
                throw new ArgumentNullException(nameof(output));

            this._useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        public Task Execute(TInput input)
        {
            if (input is null)
            {
                _output.Error("The use case input is null");
                return Task.CompletedTask;
            }

            var failures = _validator.Validate(input);
            if (failures.Any())
            {
                _output.InvalidRequest(failures);
                return Task.CompletedTask;
            }

            return _useCase.Execute(input);
        }
    }
}
