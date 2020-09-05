using FluentValidation;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsInputValidator : AbstractUseCaseValidator<GetCollectionStatisticsInput>
    {
        public GetCollectionStatisticsInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty();
        }
    }
}
