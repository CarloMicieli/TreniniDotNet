using System;
using System.Linq;
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

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public sealed class EditCollectionItemUseCase :
        AbstractUseCase<EditCollectionItemInput, EditCollectionItemOutput, IEditCollectionItemOutputPort>
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public EditCollectionItemUseCase(
            IUseCaseInputValidator<EditCollectionItemInput> inputValidator,
            IEditCollectionItemOutputPort output,
            CollectionsService collectionService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditCollectionItemInput input)
        {
            var owner = new Owner(input.Owner);
            var collectionId = new CollectionId(input.Id);
            var itemId = new CollectionItemId(input.ItemId);

            var collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner, collectionId);
                return;
            }

            var item = collection.FindItemById(itemId);
            if (item is null)
            {
                OutputPort.CollectionItemNotFound(owner, collectionId, itemId);
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

            var addedDate = input.AddedDate.ToLocalDateOrDefault();
            var condition = EnumHelpers.OptionalValueFor<Condition>(input.Condition);

            var price = input.Price?.ToPriceOrDefault();

            var modifiedItem = item.With(
                catalogItem: item.CatalogItem,
                condition: condition,
                price: price,
                addedDate: addedDate,
                notes: input.Notes);
            collection.UpdateItem(modifiedItem);

            await _collectionService.UpdateCollectionAsync(collection);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditCollectionItemOutput(collectionId, itemId, item.CatalogItem.Slug));
        }
    }
}
