using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandsList
{
    public sealed class GetBrandsListHandler : AsyncRequestHandler<GetBrandsListRequest>
    {
        private readonly IGetBrandsListUseCase _useCase;

        public GetBrandsListHandler(IGetBrandsListUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(GetBrandsListRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetBrandsListInput(request.Page));
        }
    }
}
