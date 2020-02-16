using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.CreateBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateBrand : IUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly BrandService _brandService;

        public CreateBrand(BrandService brandService, IOutputPort outputPort, IUnitOfWork unitOfWork)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _brandService = brandService;
        }

        public async Task Execute(CreateBrandInput input)
        {
            bool brandExists = await _brandService.BrandAlreadyExists(input.Name);
            if (brandExists)
            {
                _outputPort.BrandAlreadyExists($"Brand '{input.Name}' already exists");
                return;
            }

            IBrand brand = await _brandService.CreateBrand(
                input.Name, 
                input.CompanyName,
                input.WebsiteUrl,
                input.EmailAddress, 
                input.BrandKind ?? BrandKind.Industrial);

            await _unitOfWork.Save();

            BuildOutput(brand);    
        }

        public void BuildOutput(IBrand brand)
        {
            var output = new CreateBrandOutput(brand);
            _outputPort.Standard(output);
        }
    }
}
