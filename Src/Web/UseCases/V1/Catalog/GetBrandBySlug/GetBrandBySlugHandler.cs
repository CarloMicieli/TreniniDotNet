using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetBrandBySlug
{
    public class GetBrandBySlugHandler : AsyncRequestHandler<GetBrandBySlugRequest>
    {
        private readonly IGetBrandBySlugUseCase _useCase;

        public GetBrandBySlugHandler(IGetBrandBySlugUseCase useCase)
        {
            _useCase = useCase ??
                throw new ArgumentNullException(nameof(useCase));
        }

        protected override Task Handle(GetBrandBySlugRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetBrandBySlugInput(request.Slug));
        }
    }
}
