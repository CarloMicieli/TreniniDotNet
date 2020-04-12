using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScalesList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScalesList
{
    public class GetScalesListHandler : AsyncRequestHandler<GetScalesListRequest>
    {
        private readonly IGetScalesListUseCase _useCase;

        public GetScalesListHandler(IGetScalesListUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(GetScalesListRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetScalesListInput(request.Page));
        }
    }
}
