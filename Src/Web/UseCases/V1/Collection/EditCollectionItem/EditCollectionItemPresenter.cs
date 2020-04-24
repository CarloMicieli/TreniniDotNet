using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.EditCollectionItem;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.EditCollectionItem
{
    public sealed class EditCollectionItemPresenter : DefaultHttpResultPresenter<EditCollectionItemOutput>, IEditCollectionItemOutputPort
    {
        public void CollectionItemNotFound(Owner owner, CollectionId id, CollectionItemId itemId)
        {
            ViewModel = new NotFoundResult();
        }

        public void CollectionNotFound(Owner owner, CollectionId id)
        {
            ViewModel = new NotFoundResult();
        }

        public void ShopNotFound(string shop)
        {
            ViewModel = new NotFoundResult();
        }

        public override void Standard(EditCollectionItemOutput output)
        {
            ViewModel = new OkResult();
        }
    }
}
