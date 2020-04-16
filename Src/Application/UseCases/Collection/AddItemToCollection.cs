using System;
using System.Threading.Tasks;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Application.Boundaries.Collection.AddItemToCollection;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Common.Enums;
using NodaMoney;
using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collection.Shops;

namespace TreniniDotNet.Application.UseCases.Collection
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
            var collectionId = new CollectionId(input.Id);

            bool collectionExists = await _collectionService.ExistAsync(collectionId);
            if (!collectionExists)
            {
                OutputPort.CollectionNotFound($"Collection with id {collectionId} does not exist");
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

            var condition = EnumHelpers.OptionalValueFor<Condition>(input.Condition) ?? Condition.New;
            var addedDate = input.AddedDate.ToLocalDate();

            var catalogItem = Domain.Collection.Shared.CatalogItem.Of(
                input.Brand,
                new ItemNumber(input.ItemNumber));

            var itemId = await _collectionService.AddItemAsync(
                collectionId,
                catalogItem,
                condition,
                Money.Euro(input.Price), //TODO: fixme
                addedDate,
                shopInfo,
                input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(
                new AddItemToCollectionOutput(collectionId, itemId, catalogItem.Slug));
        }
    }
}
