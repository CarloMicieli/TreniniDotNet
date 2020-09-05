using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;

namespace TreniniDotNet.Application.Catalog.Scales
{
    public static class NewCreateScaleInput
    {
        public static readonly CreateScaleInput Empty = With();

        public static CreateScaleInput With(
            string name = null,
            decimal? ratio = null,
            ScaleGaugeInput scaleGauge = null,
            string description = null,
            List<string> standards = null,
            int? weight = null) =>
            new CreateScaleInput(name, ratio, scaleGauge, description, standards, weight);
    }
}
