using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemPresenter : DefaultHttpResultPresenter<EditCollectionItemOutput>, IEditCollectionItemOutputPort
    {
        public override void Standard(EditCollectionItemOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
