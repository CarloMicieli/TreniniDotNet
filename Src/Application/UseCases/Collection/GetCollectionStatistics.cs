using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class GetCollectionStatistics : ValidatedUseCase<GetCollectionStatisticsInput, IGetCollectionStatisticsOutputPort>, IGetCollectionStatisticsUseCase
    {
        public GetCollectionStatistics(IGetCollectionStatisticsOutputPort output)
            : base(new GetCollectionStatisticsInputValidator(), output)
        {
        }

        protected override Task Handle(GetCollectionStatisticsInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
