using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetRailwayBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetRailwayBySlug
{
    public class GetRailwayBySlugHandler : AsyncRequestHandler<GetRailwayBySlugRequest>
    {
        private readonly IGetRailwayBySlugUseCase _useCase;

        public GetRailwayBySlugHandler(IGetRailwayBySlugUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(GetRailwayBySlugRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetRailwayBySlugInput(request.Slug));
        }
    }
}
