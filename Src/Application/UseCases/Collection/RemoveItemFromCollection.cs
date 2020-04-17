using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.RemoveItemFromCollection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Extensions;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class RemoveItemFromCollection : ValidatedUseCase<RemoveItemFromCollectionInput, IRemoveItemFromCollectionOutputPort>, IRemoveItemFromCollectionUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromCollection(
            IRemoveItemFromCollectionOutputPort output,
            CollectionsService collectionService,
            IUnitOfWork unitOfWork)
            : base(new RemoveItemFromCollectionInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override async Task Handle(RemoveItemFromCollectionInput input)
        {
            var id = new CollectionId(input.Id);
            var itemId = new CollectionItemId(input.ItemId);

            var exists = await _collectionService.ItemExistsAsync(id, itemId);
            if (!exists)
            {
                OutputPort.CollectionItemNotFound(id, itemId);
                return;
            }

            await _collectionService.RemoveItemAsync(
                id,
                itemId,
                input.Removed.ToLocalDateOrDefault(),
                input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveItemFromCollectionOutput(id, itemId));
        }
    }
}
