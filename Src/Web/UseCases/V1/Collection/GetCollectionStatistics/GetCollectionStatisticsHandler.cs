using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics;

namespace TreniniDotNet.Web.UseCases.V1.Collection.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsHandler : UseCaseHandler<IGetCollectionStatisticsUseCase, GetCollectionStatisticsRequest, GetCollectionStatisticsInput>
    {
        public GetCollectionStatisticsHandler(IGetCollectionStatisticsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
