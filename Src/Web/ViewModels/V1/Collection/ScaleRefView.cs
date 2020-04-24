using TreniniDotNet.Domain.Collection.Shared;

namespace TreniniDotNet.Web.ViewModels.V1.Collection
{
    public sealed class ScaleRefView
    {
        private readonly IScaleRef _scale;

        public ScaleRefView(IScaleRef scale)
        {
            _scale = scale;
        }

        public string Slug => _scale.Slug;
        public string Value => _scale.Name;
    }
}
