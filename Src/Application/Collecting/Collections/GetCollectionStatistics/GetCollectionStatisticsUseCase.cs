using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsUseCase :
        AbstractUseCase<GetCollectionStatisticsInput, GetCollectionStatisticsOutput, IGetCollectionStatisticsOutputPort>
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionStatisticsUseCase(
            IUseCaseInputValidator<GetCollectionStatisticsInput> inputValidator,
            IGetCollectionStatisticsOutputPort output,
            CollectionsService collectionService)
            : base(inputValidator, output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
        }

        protected override async Task Handle(GetCollectionStatisticsInput input)
        {
            var owner = new Owner(input.Owner);

            var collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            var statistics = CollectionStats.FromCollection(collection);
            OutputPort.Standard(new GetCollectionStatisticsOutput(statistics));
        }
    }
}
