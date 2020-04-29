using FluentValidation;

namespace TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics
{
    public sealed class GetCollectionStatisticsInputValidator : AbstractValidator<GetCollectionStatisticsInput>
    {
        public GetCollectionStatisticsInputValidator()
        {
            RuleFor(x => x.Owner)
                .NotEmpty();
        }
    }
}
