using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandHandler : AsyncRequestHandler<CreateBrandRequest>
    {
        private readonly ICreateBrandUseCase _useCase;
        private readonly IMapper _mapper;

        public CreateBrandHandler(ICreateBrandUseCase useCase, IMapper mapper)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        protected override Task Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var input = _mapper.Map<CreateBrandInput>(request);
            return _useCase.Execute(input);
        }
    }
}
