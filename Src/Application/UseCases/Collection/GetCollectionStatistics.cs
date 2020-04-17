using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class GetCollectionStatistics :
        ValidatedUseCase<GetCollectionStatisticsInput, IGetCollectionStatisticsOutputPort>,
        IGetCollectionStatisticsUseCase
    {
        private readonly CollectionsService _collectionService;

        public GetCollectionStatistics(IGetCollectionStatisticsOutputPort output, CollectionsService collectionService)
            : base(new GetCollectionStatisticsInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException(nameof(collectionService));
        }

        protected override async Task Handle(GetCollectionStatisticsInput input)
        {
            var owner = new Owner(input.Owner);

            ICollection collection = await _collectionService.GetCollectionByUserAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }
        }
    }
}
