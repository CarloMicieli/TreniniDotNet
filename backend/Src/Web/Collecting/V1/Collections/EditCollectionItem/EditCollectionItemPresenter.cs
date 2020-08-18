using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem
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
