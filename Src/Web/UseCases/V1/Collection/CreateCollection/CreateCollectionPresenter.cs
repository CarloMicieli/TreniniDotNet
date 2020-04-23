using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Web.ViewModels;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateCollection
{
    public sealed class CreateCollectionPresenter : DefaultHttpResultPresenter<CreateCollectionOutput>, ICreateCollectionOutputPort
    {
        public override void Standard(CreateCollectionOutput output)
        {
            ViewModel = Created(
                nameof(GetCollectionByOwner.CollectionsController.GetCollectionByOwner),
                new
                {
                    version = "1",
                },
                output);
        }

        public void UserHasAlreadyOneCollection(string message)
        {
            ViewModel = new ConflictObjectResult(message);
        }
    }
}
