using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Common.Enums;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collection.Shops;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Common;
using NodaMoney;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public sealed class AddItemToCollection :
        ValidatedUseCase<AddItemToCollectionInput, IAddItemToCollectionOutputPort>,
        IAddItemToCollectionUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public AddItemToCollection(IAddItemToCollectionOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
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
