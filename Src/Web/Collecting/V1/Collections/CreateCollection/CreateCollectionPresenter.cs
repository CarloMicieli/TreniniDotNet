﻿using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Web.Infrastructure.ViewModels;

namespace TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection
{
    public sealed class CreateCollectionPresenter : DefaultHttpResultPresenter<CreateCollectionOutput>, ICreateCollectionOutputPort
    {
        public override void Standard(CreateCollectionOutput output)
        {
            ViewModel = Created(
                nameof(GetCollectionByOwner.CollectionsController.GetCollectionByOwner),
                new
                {
                    id = output.Id,
                    version = "1",
                },
                output);
        }

        public void UserHasAlreadyOneCollection(Owner owner)
        {
            ViewModel = new ConflictObjectResult($"User {owner} has already one collection");
        }
    }
}
