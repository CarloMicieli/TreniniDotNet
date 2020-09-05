using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Web.Infrastructure.UseCases
{
    public abstract class UseCaseHandler<TUseCase, TRequest, TUseCaseInput> : AsyncRequestHandler<TRequest>
        where TRequest : IRequest
        where TUseCaseInput : IUseCaseInput
        where TUseCase : IUseCase<TUseCaseInput>
    {
        protected TUseCase UseCase { get; }
        protected IMapper Mapper { get; }

        protected UseCaseHandler(TUseCase useCase, IMapper mapper)
        {
            UseCase = useCase ??
                      throw new ArgumentNullException(nameof(useCase));
            Mapper = mapper ??
                     throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task Handle(TRequest request, CancellationToken cancellationToken)
        {
            var input = Mapper.Map<TUseCaseInput>(request);
            return UseCase.Execute(input);
        }
    }
}