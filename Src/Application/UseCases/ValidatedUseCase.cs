using FluentValidation;
using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class ValidatedUseCase<TInput, TOutputPort>
        where TInput : IUseCaseInput
        where TOutputPort : IOutputPortErrors
    {
        private readonly IUseCaseInputValidator<TInput> _validator;

        protected ValidatedUseCase(IValidator<TInput> validator, TOutputPort output)
        {
            this._validator = new UseCaseInputValidator<TInput>(validator);

            this.OutputPort = output ??
                throw new ArgumentNullException(nameof(output));
        }

        public Task Execute(TInput input)
        {
            if (input is null)
            {
                OutputPort.Error("The use case input is null");
                return Task.CompletedTask;
            }

            var failures = _validator.Validate(input);
            if (failures.Any())
            {
                OutputPort.InvalidRequest(failures);
                return Task.CompletedTask;
            }

            return Handle(input);
        }

        protected abstract Task Handle(TInput input);

        protected TOutputPort OutputPort { get; }
    }
}
