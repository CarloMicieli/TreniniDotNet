using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;
using TreniniDotNet.Domain.Collecting.ValueObjects;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public sealed class CreateCollectionUseCase : ValidatedUseCase<CreateCollectionInput, ICreateCollectionOutputPort>, ICreateCollectionUseCase
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCollectionUseCase(ICreateCollectionOutputPort output, CollectionsService collectionService, IUnitOfWork unitOfWork)
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
