using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerUseCase : AbstractUseCase<GetCollectionByOwnerInput, GetCollectionByOwnerOutput, IGetCollectionByOwnerOutputPort>
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionByOwnerUseCase(
            IUseCaseInputValidator<GetCollectionByOwnerInput> inputValidator,
            IGetCollectionByOwnerOutputPort output,
            CollectionsService collectionService)
            : base(inputValidator, output)
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
