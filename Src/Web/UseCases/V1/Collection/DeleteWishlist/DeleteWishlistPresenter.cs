﻿using TreniniDotNet.Application.Boundaries.Collection.DeleteWishlist;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.DeleteWishlist
{
    public sealed class DeleteWishlistPresenter : DefaultHttpResultPresenter<DeleteWishlistOutput>, IDeleteWishlistOutputPort
    {
        public override void Standard(DeleteWishlistOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}