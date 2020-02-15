using MediatR;
using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.CreateBrand;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    public class CreateBrandHandler : AsyncRequestHandler<CreateBrandRequest>
    {
        private readonly IUseCase _useCase;

        public CreateBrandHandler(IUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(CreateBrandRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateBrandInput(
                request.Name,
                request.CompanyName,
                request.WebsiteUrl != null ? new Uri(request.WebsiteUrl) : null,
                request.EmailAddress != null ? new MailAddress(request.EmailAddress) : null,
                request.BrandType.ToBrandKind());

            return _useCase.Execute(input);
        }
    }
}
