using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Scales.EditScale;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Catalog.V1.Scales.EditScale
{
    public sealed class EditScalePresenter : DefaultHttpResultPresenter<EditScaleOutput>, IEditScaleOutputPort
    {
        public void ScaleNotFound(Slug slug)
        {
            ViewModel = new NotFoundObjectResult(new { Slug = slug });
        }

        public override void Standard(EditScaleOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
