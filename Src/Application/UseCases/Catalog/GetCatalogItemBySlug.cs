using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetCatalogItemBySlug;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public class GetCatalogItemBySlug : IGetCatalogItemBySlugUseCase
    {
        private readonly IGetCatalogItemBySlugOutputPort _outputPort;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CatalogItemService _catalogItemService;

        public GetCatalogItemBySlug(IGetCatalogItemBySlugOutputPort outputPort, IUnitOfWork unitOfWork, CatalogItemService catalogItemService)
        {
            _outputPort = outputPort;
            _unitOfWork = unitOfWork;
            _catalogItemService = catalogItemService;
        }

        public Task Execute(GetCatalogItemBySlugInput input)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
