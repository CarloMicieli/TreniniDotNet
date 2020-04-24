using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateCollection;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Domain.Collection.Collections;
using TreniniDotNet.Domain.Collection.Shared;
using TreniniDotNet.Domain.Collection.ValueObjects;

namespace TreniniDotNet.Application.UseCases.Collection.Collections
{
    public sealed class CreateCollection : ValidatedUseCase<CreateCollectionInput, ICreateCollectionOutputPort>, ICreateCollectionUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCollection(ICreateCollectionOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
            : base(new CreateCollectionInputValidator(), output)
        {
            _collectionService = collectionService ??
                throw new ArgumentNullException();
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException();
        }

        protected override async Task Handle(CreateCollectionInput input)
        {
            var owner = new Owner(input.Owner);

            var userHasCollection = await _collectionService.UserAlredyOwnCollectionAsync(owner);
            if (userHasCollection)
            {
                OutputPort.UserHasAlreadyOneCollection("The user already owns a collection");
                return;
            }

            var id = await _collectionService.CreateAsync(input.Owner, input.Notes);

            var _ = await _unitOfWork.SaveAsync();

            CollectionCreated(id, input.Owner);
        }

        private void CollectionCreated(CollectionId id, string owner) =>
            OutputPort.Standard(new CreateCollectionOutput(id.ToGuid(), owner));
    }
}
