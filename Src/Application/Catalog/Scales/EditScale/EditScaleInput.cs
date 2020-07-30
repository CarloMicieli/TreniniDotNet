using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales.EditScale
{
    public sealed class EditScaleInput : IUseCaseInput
    {
        public EditScaleInput(
            Slug slug,
            string? name,
            decimal? ratio,
            ScaleGaugeInput? gauge,
            string? description,
            List<string> standards,
            int? weight)
        {
            ScaleSlug = slug;
            Values = new ModifiedScaleValues(
                name,
                ratio,
                gauge,
                description,
                standards,
                weight);
        }

        public Slug ScaleSlug { get; }
        public ModifiedScaleValues Values { get; }
    }
}
