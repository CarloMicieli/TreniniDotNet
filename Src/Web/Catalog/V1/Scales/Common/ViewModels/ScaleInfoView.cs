using System;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels
{
    public sealed class ScaleInfoView
    {
        private readonly IScaleInfo _scale;

        public ScaleInfoView(IScaleInfo scale)
        {
            _scale = scale ??
                throw new ArgumentNullException(nameof(scale));
        }

        public Guid Id => _scale.ScaleId.ToGuid();

        public string Name => _scale.Name;

        public string Slug => _scale.Slug.ToString();
    }
}
