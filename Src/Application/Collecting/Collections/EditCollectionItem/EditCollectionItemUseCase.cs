using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.EditCollectionItem
{
    public sealed class EditCollectionItemUseCase :
        ValidatedUseCase<EditCollectionItemInput, IEditCollectionItemOutputPort>,
        IEditCollectionItemUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public EditCollectionItemUseCase(IEditCollectionItemOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
            : base(new EditCollectionItemInputValidator(), output)
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

            bool collectionExists = await _collectionService.ExistAsync(owner, collectionId);
            if (!collectionExists)
            {
                OutputPort.CollectionNotFound(owner, collectionId);
                return;
            }

            ICollectionItem? item = await _collectionService.GetItemByIdAsync(collectionId, itemId);
            if (item is null)
            {
                OutputPort.CollectionItemNotFound(owner, collectionId, itemId);
                return;
            }

            IShopInfo? shopInfo = null;
            if (!string.IsNullOrWhiteSpace(input.Shop))
            {
                shopInfo = await _collectionService.GetShopInfo(input.Shop);
                if (shopInfo is null)
                {
                    OutputPort.ShopNotFound(input.Shop);
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
