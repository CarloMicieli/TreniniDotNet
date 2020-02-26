using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwaysList;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwaysList
{
    public class GetRailwaysListHandler : AsyncRequestHandler<GetRailwaysListRequest>
    {
        private readonly IGetRailwaysListUseCase _useCase;

        public GetRailwaysListHandler(IGetRailwaysListUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(GetRailwaysListRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetRailwaysListInput(request.Page));
        }
    }
}
