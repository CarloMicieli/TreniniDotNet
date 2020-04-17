using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public sealed class EditCollectionItem :
        ValidatedUseCase<EditCollectionItemInput, IEditCollectionItemOutputPort>,
        IEditCollectionItemUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public EditCollectionItem(IEditCollectionItemOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
            : base(new EditCollectionItemInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(EditCollectionItemInput input)
        {
            var collectionId = new CollectionId(input.Id);
            var itemId = new CollectionItemId(input.ItemId);

            bool collectionExists = await _collectionService.ExistAsync(collectionId);
            if (!collectionExists)
            {
                OutputPort.CollectionNotFound($"Collection with id {collectionId} does not exist");
                return;
            }

            ICollectionItem item = await _collectionService.GetItemByIdAsync(collectionId, itemId);
            if (item is null)
            {
                OutputPort.CollectionItemNotFound($"Collection item with id {itemId} does not exist");
                return;
            }

            IShopInfo? shopInfo = null;
            if (!string.IsNullOrWhiteSpace(input.Shop))
            {
                shopInfo = await _collectionService.GetShopInfo(input.Shop);
                if (shopInfo is null)
                {
                    OutputPort.ShopNotFound($"Shop '{input.Shop}' does not exist");
                }
            }

            var addedDate = input.AddedDate.ToLocalDateOrDefault();
            var condition = EnumHelpers.OptionalValueFor<Condition>(input.Condition);

            await _collectionService.EditItemAsync(
                collectionId,
                itemId,
                item.CatalogItem,
                condition ?? item.Condition,
                input.Price ?? item.Price,
                addedDate ?? item.AddedDate,
                null,
                input.Notes ?? item.Notes);

            await _unitOfWork.SaveAsync();

            OutputPort.Standard(new EditCollectionItemOutput(collectionId, itemId, item.CatalogItem.Slug));
        }
    }
}
