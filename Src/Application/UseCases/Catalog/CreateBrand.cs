using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateBrand : ValidatedUseCase<CreateBrandInput, ICreateBrandOutputPort>, ICreateBrandUseCase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreateBrandOutputPort _outputPort;

        public CreateBrand(
            IUseCaseInputValidator<CreateBrandInput> validator, 
            ICreateBrandOutputPort outputPort,
            BrandService brandService,
            IUnitOfWork unitOfWork)
            : base(validator, outputPort)
        {
            _brandService = brandService ??
                throw new ArgumentNullException(nameof(brandService));

            _unitOfWork = unitOfWork ?? 
                throw new ArgumentNullException(nameof(unitOfWork));

            _outputPort = outputPort ??
                throw new ArgumentNullException(nameof(outputPort));
        }

        protected override async Task Handle(CreateBrandInput input)
        {
            string brandName = input.Name!;
            Slug slug = Slug.Of(brandName);

            bool brandExists = await _brandService.BrandAlreadyExists(slug);
            if (brandExists)
            {
                _outputPort.BrandAlreadyExists($"Brand '{brandName}' already exists");
                return;
            }

            var _ = await _brandService.CreateBrand(
                brandName,
                slug,
                input.CompanyName,
                input.WebsiteUrl,
                input.EmailAddress,
                input.Kind.ToBrandKind());

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateBrandOutput(slug);
            _outputPort.Standard(output);
        }
    }
}
