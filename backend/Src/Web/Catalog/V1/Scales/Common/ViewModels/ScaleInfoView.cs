using System;
using TreniniDotNet.Domain.Catalog.CatalogItems;

namespace TreniniDotNet.Web.Catalog.V1.Scales.Common.ViewModels
{
    public sealed class ScaleInfoView
    {
        private readonly ScaleRef _scale;

        public ScaleInfoView(ScaleRef scale)
        {
            _scale = scale ??
                throw new ArgumentNullException(nameof(scale));
        }

        public Guid Id => _scale.Id.ToGuid();

        public string Name => _scale.ToString();

        public string Slug => _scale.Slug;
    }
}
