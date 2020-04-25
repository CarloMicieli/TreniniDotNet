using System.Collections.Generic;
using TreniniDotNet.Application.Boundaries.Common;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
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

        public List<string> Standards { get; } = new List<string>();

        public int? Weight { get; }
    }
}