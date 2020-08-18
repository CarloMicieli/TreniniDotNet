using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsHandler : UseCaseHandler<GetCollectionStatisticsUseCase, GetCollectionStatisticsRequest, GetCollectionStatisticsInput>
    {
        public GetCollectionStatisticsHandler(GetCollectionStatisticsUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
