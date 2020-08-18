using System.Collections.Generic;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.SharedKernel.Slugs;

namespace TreniniDotNet.Application.Catalog.Scales
{
    public static class NewEditScaleInput
    {
        public static readonly EditScaleInput Empty = With();

        public static EditScaleInput With(
            Slug? scaleSlug = null,
            string name = null,
            decimal? ratio = null,
            ScaleGaugeInput gauge = null,
            string description = null,
            List<string> standards = null,
            int? weight = null) =>
            new EditScaleInput(scaleSlug ?? Slug.Empty, name, ratio, gauge, description, standards, weight);
    }
}
