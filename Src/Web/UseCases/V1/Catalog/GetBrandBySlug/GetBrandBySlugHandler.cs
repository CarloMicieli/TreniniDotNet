using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.GetBrandBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugHandler : AsyncRequestHandler<GetBrandBySlugRequest>
    {
        private readonly IUseCase _useCase;

        public GetBrandBySlugHandler(IUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(GetBrandBySlugRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetBrandBySlugInput(request.Slug));
        }
    }
}
