﻿using TreniniDotNet.Application.Boundaries.Collection.CreateShop;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateShop
{
    public sealed class CreateShopPresenter : DefaultHttpResultPresenter<CreateShopOutput>, ICreateShopOutputPort
    {
        public override void Standard(CreateShopOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}