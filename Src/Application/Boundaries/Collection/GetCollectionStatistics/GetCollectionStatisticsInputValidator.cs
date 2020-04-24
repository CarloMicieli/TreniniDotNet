using FluentValidation;

namespace TreniniDotNet.Application.Boundaries.Collection.GetCollectionStatistics
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
