using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.EditBrand;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog.Brands
{
    public sealed class EditBrand : ValidatedUseCase<EditBrandInput, IEditBrandOutputPort>, IEditBrandUseCase
    {
        private readonly BrandService _brandService;
        private readonly IUnitOfWork _unitOfWork;

        public EditBrand(
            IEditBrandOutputPort output,
            BrandService brandService,
            IUnitOfWork unitOfWork)
            : base(new EditBrandInputValidator(), output)
        {
            _brandService = brandService ??
                throw new ArgumentNullException(nameof(brandService));

            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override Task Handle(EditBrandInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
