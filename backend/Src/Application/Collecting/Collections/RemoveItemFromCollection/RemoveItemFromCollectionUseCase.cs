using System;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;

namespace TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection
{
    public sealed class RemoveItemFromCollectionUseCase : AbstractUseCase<RemoveItemFromCollectionInput, RemoveItemFromCollectionOutput, IRemoveItemFromCollectionOutputPort>
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveItemFromCollectionUseCase(
            IUseCaseInputValidator<RemoveItemFromCollectionInput> inputValidator,
            IRemoveItemFromCollectionOutputPort output,
            CollectionsService collectionService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
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

            var collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(id);
                return;
            }

            var item = collection.FindItemById(itemId);
            if (item is null)
            {
                OutputPort.CollectionItemNotFound(id, itemId);
                return;
            }

            collection.RemoveItem(itemId);
            await _collectionService.UpdateCollectionAsync(collection);

            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new RemoveItemFromCollectionOutput(id, itemId));
        }
    }
}
