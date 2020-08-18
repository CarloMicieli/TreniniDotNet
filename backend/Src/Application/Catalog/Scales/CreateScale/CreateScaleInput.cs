using System.Collections.Generic;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;

namespace TreniniDotNet.Application.Catalog.Scales.CreateScale
{
    public sealed class CreateScaleInput : IUseCaseInput
    {
        public CreateScaleInput(
            string? name,
            decimal? ratio,
            ScaleGaugeInput? gauge,
            string? description,
            List<string> standards,
            int? weight)
        {
            Name = name;
            Ratio = ratio;
            Gauge = gauge ?? ScaleGaugeInput.Default();
            Description = description;
            Standards = standards;
            Weight = weight;
        }

        public string? Name { get; }

        public decimal? Ratio { get; }

        public ScaleGaugeInput Gauge { get; }

        public string? Description { get; }

        public List<string> Standards { get; }

        public int? Weight { get; }
    }
}
