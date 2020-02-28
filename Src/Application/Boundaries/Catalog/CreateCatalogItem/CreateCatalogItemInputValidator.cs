using FluentValidation;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class CreateCatalogItemInputValidator : AbstractValidator<CreateCatalogItemInput>
    {
        public CreateCatalogItemInputValidator()
        {
            RuleFor(x => x.BrandName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.ItemNumber)
                .NotEmpty()
                .NotNull()
                .MinimumLength(4)
                .MaximumLength(10);

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .MaximumLength(250);

            RuleFor(x => x.PrototypeDescription)
                .MaximumLength(2500);                
            
            RuleFor(x => x.ModelDescription)
                .MaximumLength(2500);   

            RuleFor(x => x.PowerMethod)  
                .NotNull()
                .IsEnumName(typeof(PowerMethod), caseSensitive: false);

            RuleFor(x => x.Scale)
                .NotEmpty()
                .NotNull()
                .MaximumLength(10);

            RuleFor(x => x.RollingStocks)
                .NotNull()
                .NotEmpty();
            
            RuleForEach(x => x.RollingStocks)                
                .SetValidator(new RollingStockInputValidator());
        }

        public sealed class RollingStockInputValidator : AbstractValidator<RollingStockInput>
        {
            public RollingStockInputValidator()
            {
                RuleFor(x => x.Era)
                    .NotNull()
                    .IsEnumName(typeof(Era), caseSensitive: false);

                RuleFor(x => x.Category)
                    .NotNull()
                    .IsEnumName(typeof(Category), caseSensitive: false);

                RuleFor(x => x.Length)
                    .GreaterThanOrEqualTo(0M);

                RuleFor(x => x.Railway)
                    .NotEmpty()
                    .NotNull()
                    .MinimumLength(2)
                    .MaximumLength(25);

                RuleFor(x => x.RoadNumber)
                    .MaximumLength(25);
                    
                RuleFor(x => x.ClassName)
                    .MaximumLength(25);                    
            }
        }
    }
}