using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
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
            bool brandExists = await _brandService.BrandAlreadyExists(input.Name!);
            if (brandExists)
            {
                _outputPort.BrandAlreadyExists($"Brand '{input.Name}' already exists");
                return;
            }

            IBrand brand = await _brandService.CreateBrand(
                input.Name!,
                input.CompanyName,
                input.WebsiteUrl,
                input.EmailAddress,
                input.Kind.ToBrandKind());

            await _unitOfWork.SaveAsync();

            BuildOutput(brand);
        }

        private void BuildOutput(IBrand brand)
        {
            var output = new CreateBrandOutput(brand);
            _outputPort.Standard(output);
        }
    }
}
