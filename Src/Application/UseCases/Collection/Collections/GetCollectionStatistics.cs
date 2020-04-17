using NodaMoney;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
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

            ICollection collection = await _collectionService.GetByOwnerAsync(owner);
            if (collection is null)
            {
                OutputPort.CollectionNotFound(owner);
                return;
            }

            var items = collection.Items
                .Select(it => new
                {
                    Count = it.Details?.RollingStocksCount ?? 1,
                    Category = it.Details?.Category ?? CollectionCategory.Unspecified,
                    Year = Year.FromLocalDate(it.AddedDate),
                    it.Price
                })
                .GroupBy(it => new { it.Category, it.Year })
                .Select(it => (ICollectionStatsItem)new CollectionStatsItem(
                    it.Key.Year,
                    it.Key.Category,
                    it.Sum(y => y.Count),
                    it.Select(y => y.Price).Sum()))
                .ToImmutableList();

            var totalValue = items.Select(it => it.TotalValue).Sum();

            ICollectionStats statistics = new CollectionStats(
                collection.Owner,
                collection.ModifiedDate ?? collection.CreatedDate,
                totalValue,
                items);

            OutputPort.Standard(new GetCollectionStatisticsOutput(statistics));
        }
    }

    public static class MoneyExtensions
    {
        public static Money Sum(this IEnumerable<Money> en)
        {
            Money? zero = en.FirstOrDefault();
            if (zero.HasValue)
            {
                return en.Skip(1).Aggregate(zero.Value, (acc, l) => acc + l);
            }

            return Money.Euro(0M);
        }
    }
}
