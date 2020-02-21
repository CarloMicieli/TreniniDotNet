using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{ 
    public sealed class CreateBrand : AbstractUseCase<CreateBrandInput, CreateBrandOutput, ICreateBrandOutputPort>, ICreateBrandUseCase
    {
        private readonly BrandService _brandService;

        public CreateBrand(
            IEnumerable<IValidator<CreateBrandInput>> validators, 
            ICreateBrandOutputPort outputPort,
            BrandService brandService,
            IUnitOfWork unitOfWork)
            : base(validators, outputPort, unitOfWork)
        {
            _brandService = brandService;
        }

        protected override async Task ExecuteUseCase(CreateBrandInput input)
        {
            bool brandExists = await _brandService.BrandAlreadyExists(input.Name!);
            if (brandExists)
            {
                OutputPort.BrandAlreadyExists($"Brand '{input.Name}' already exists");
                return;
            }

            IBrand brand = await _brandService.CreateBrand(
                input.Name!,
                input.CompanyName,
                input.WebsiteUrl,
                input.EmailAddress,
                input.Kind.ToBrandKind());

            await UnitOfWork.SaveAsync();

            BuildOutput(brand);
        }

        private void BuildOutput(IBrand brand)
        {
            var output = new CreateBrandOutput(brand);
            OutputPort.Standard(output);
        }
    }
}
