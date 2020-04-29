using System;
using System.Threading.Tasks;
using NodaMoney;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.Shops;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.AddItemToCollection
{
    public sealed class AddItemToCollectionUseCase :
        ValidatedUseCase<AddItemToCollectionInput, IAddItemToCollectionOutputPort>,
        IAddItemToCollectionUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToCollectionUseCase(IAddItemToCollectionOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
            : base(new AddItemToCollectionInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(AddItemToCollectionInput input)
        {
            var owner = new Owner(input.Owner);

            CollectionId? collectionId = await _collectionService.GetIdByOwnerAsync(owner);
            if (collectionId.HasValue == false)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            IShopInfo? shopInfo = null;
            if (!string.IsNullOrWhiteSpace(input.Shop))
            {
                shopInfo = await _collectionService.GetShopInfo(input.Shop);
                if (shopInfo is null)
                {
                    OutputPort.ShopNotFound(input.Shop);
                    return;
                }
            }

            var catalogItemSlug = Slug.Of(input.CatalogItem);
            var catalogItem = await _collectionService.GetCatalogRefAsync(catalogItemSlug);
            if (catalogItem is null)
            {
                OutputPort.CatalogItemNotFound(catalogItemSlug);
                return;
            }

            var condition = EnumHelpers.OptionalValueFor<Condition>(input.Condition) ?? Condition.New;
            var addedDate = input.AddedDate.ToLocalDate();

            var itemId = await _collectionService.AddItemAsync(
                collectionId.Value,
                catalogItem,
                condition,
                Money.Euro(input.Price), //TODO: fixme
                addedDate,
                shopInfo,
                input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(
                new AddItemToCollectionOutput(collectionId.Value, itemId, catalogItem.Slug));
        }
    }
}
