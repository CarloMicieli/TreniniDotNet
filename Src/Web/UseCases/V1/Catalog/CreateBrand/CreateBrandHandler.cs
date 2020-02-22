using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandHandler : AsyncRequestHandler<CreateBrandRequest>
    {
        private readonly ICreateBrandUseCase _useCase;

        public CreateBrandHandler(ICreateBrandUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateBrandInput(
                request.Name,
                request.CompanyName,
                request.WebsiteUrl,
                request.EmailAddress,
                request.BrandType);

            return _useCase.Execute(input);
        }
    }
}
