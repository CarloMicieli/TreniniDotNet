using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionUseCase : AbstractUseCase<AddItemToCollectionInput, AddItemToCollectionOutput, IAddItemToCollectionOutputPort>
    {
        private readonly CollectionsService _collectionService;
        private readonly CollectionItemsFactory _collectionItemsFactory;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToCollectionUseCase(
            IUseCaseInputValidator<AddItemToCollectionInput> inputValidator,
            IAddItemToCollectionOutputPort output,
            CollectionsService collectionService,
            CollectionItemsFactory collectionItemsFactory,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _collectionService = collectionService ?? throw new ArgumentNullException(nameof(collectionService));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _collectionItemsFactory =
                collectionItemsFactory ?? throw new ArgumentNullException(nameof(collectionItemsFactory));
        }

        protected override async Task Handle(AddItemToCollectionInput input)
        {
            var owner = new Owner(input.Owner);

            var collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            Shop? shop = null;
            if (!string.IsNullOrWhiteSpace(input.Shop))
            {
                var shopSlug = Slug.Of(input.Shop);
                shop = await _collectionService.GetShopBySlugAsync(shopSlug);
                if (shop is null)
                {
                    OutputPort.ShopNotFound(input.Shop);
                    return;
                }
            }

            var catalogItemSlug = Slug.Of(input.CatalogItem);
            var catalogItem = await _collectionService.GetCatalogItemAsync(catalogItemSlug);
            if (catalogItem is null)
            {
                OutputPort.CatalogItemNotFound(catalogItemSlug);
                return;
            }

            var condition = EnumHelpers.OptionalValueFor<Condition>(input.Condition) ?? Condition.New;
            var addedDate = input.AddedDate.ToLocalDate();

            var price = input.Price.ToPrice();

            var collectionItem = _collectionItemsFactory.CreateCollectionItem(
                catalogItem,
                condition,
                price,
                shop,
                addedDate,
                input.Notes);

            collection.AddItem(collectionItem);

            await _collectionService.UpdateCollectionAsync(collection);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(
                new AddItemToCollectionOutput(collection.Id, collectionItem.Id, catalogItem.Slug));
        }
    }
}
