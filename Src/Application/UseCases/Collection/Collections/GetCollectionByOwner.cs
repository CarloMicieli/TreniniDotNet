using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionByOwner;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public sealed class GetCollectionByOwner : ValidatedUseCase<GetCollectionByOwnerInput, IGetCollectionByOwnerOutputPort>, IGetCollectionByOwnerUseCase
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionByOwner(IGetCollectionByOwnerOutputPort output, CollectionsService collectionService)
            : base(new GetCollectionByOwnerInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
        }

        protected override async Task Handle(GetCollectionByOwnerInput input)
        {
            var owner = new Owner(input.Owner);

            var collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            OutputPort.Standard(new GetCollectionByOwnerOutput(collection));
        }
    }
}
