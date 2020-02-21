using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class AbstractUseCase<TInput, TOutput, TOutputPort>
        where TInput: IUseCaseInput
        where TOutput: IUseCaseOutput
        where TOutputPort : IOutputPortStandard<TOutput>
    {
        private readonly IEnumerable<IValidator<TInput>> _validators;
        private readonly TOutputPort _output;
        private readonly IUnitOfWork _unitOfWork;

        protected AbstractUseCase(IEnumerable<IValidator<TInput>> validators, TOutputPort output, IUnitOfWork unitOfWork)
        {
            this._validators = validators ?? throw new ArgumentNullException(nameof(output));
            this._output = output ?? throw new ArgumentNullException(nameof(output));
            this._unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public Task Execute(TInput input)
        {
            var failures = ValidateInput(input);
            if (failures.Any())
            {
                OutputPort.InvalidRequest(failures);
                return Task.CompletedTask;
            }

            return ExecuteUseCase(input);
        }

        protected abstract Task ExecuteUseCase(TInput input);

        protected TOutputPort OutputPort => _output;

        protected IUnitOfWork UnitOfWork => _unitOfWork;

        private List<ValidationFailure> ValidateInput(TInput input)
        {
            var failures = _validators
                .Select(validator => validator.Validate(input))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            return failures;
        }
    }
}
