using TreniniDotNet.Application.Boundaries.Catalog.EditScale;
using TreniniDotNet.Common;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.EditScale
{
    public sealed class EditScalePresenter : DefaultHttpResultPresenter<EditScaleOutput>, IEditScaleOutputPort
    {
        public void ScaleNotFound(Slug slug)
        {
            throw new System.NotImplementedException();
        }

        public override void Standard(EditScaleOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
