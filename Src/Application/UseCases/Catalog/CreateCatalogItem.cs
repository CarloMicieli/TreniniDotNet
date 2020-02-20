using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateCatalogItem : ICreateCatalogItemUseCase
    {
        private readonly IOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemService _catalogItemService;

        public CreateCatalogItem(IOutputPort outputPort, IUnitOfWork unitOfWork, CatalogItemService catalogItemService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _catalogItemService = catalogItemService;
        }

        public Task Execute(CreateCatalogItemInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
