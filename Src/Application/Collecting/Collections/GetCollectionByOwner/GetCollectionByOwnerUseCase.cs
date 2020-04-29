using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerUseCase : ValidatedUseCase<GetCollectionByOwnerInput, IGetCollectionByOwnerOutputPort>, IGetCollectionByOwnerUseCase
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionByOwnerUseCase(IGetCollectionByOwnerOutputPort output, CollectionsService collectionService)
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
