using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Domain;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Domain.Collecting.Collections
{
    public sealed class CollectionsService : IDomainService
    {
        private readonly ICollectionsRepository _collectionsRepository;
        private readonly IShopsRepository _shopsRepository;
        private readonly CollectionsFactory _collectionsFactory;
        private readonly ICatalogItemRefsRepository _catalogItemsRepository;

        public CollectionsService(
            CollectionsFactory collectionsFactory,
            ICollectionsRepository collectionsRepository,
            ICatalogItemRefsRepository catalogItemsRepository,
            IShopsRepository shopsRepository)
        {
            _collectionsRepository = collectionsRepository ?? throw new ArgumentNullException(nameof(collectionsRepository));
            _collectionsFactory = collectionsFactory ?? throw new ArgumentNullException(nameof(collectionsFactory));
            _shopsRepository = shopsRepository ?? throw new ArgumentNullException(nameof(shopsRepository));
            _catalogItemsRepository =
                catalogItemsRepository ?? throw new ArgumentNullException(nameof(catalogItemsRepository));
        }

        public Task<Collection?> GetByOwnerAsync(Owner owner) =>
            _collectionsRepository.GetByOwnerAsync(owner);

        public Task<Shop?> GetShopBySlugAsync(Slug slug) =>
            _shopsRepository.GetBySlugAsync(slug);

        public Task<CatalogItemRef?> GetCatalogItemAsync(Slug slug) =>
            _catalogItemsRepository.GetCatalogItemAsync(slug);

        public Task UpdateCollectionAsync(Collection collection) =>
            _collectionsRepository.UpdateAsync(collection);

        public Task<bool> ExistsByOwnerAsync(Owner owner) =>
            _collectionsRepository.ExistsAsync(owner);

        public Task<CollectionId> CreateAsync(Owner owner, string? notes)
        {
            var collection = _collectionsFactory.CreateCollection(owner, notes);
            return _collectionsRepository.AddAsync(collection);
        }

        public Task<Collection?> GetCollectionByIdAsync(CollectionId id) => _collectionsRepository.GetByIdAsync(id);
    }
}
