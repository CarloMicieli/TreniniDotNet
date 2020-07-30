using System;
using System.Threading.Tasks;
using TreniniDotNet.Common.Data;
using TreniniDotNet.Common.UseCases;
using TreniniDotNet.Common.UseCases.Boundaries.Inputs;
using TreniniDotNet.Domain.Collecting.Collections;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Application.Collecting.Collections.CreateCollection
{
    public sealed class CreateCollectionUseCase : AbstractUseCase<CreateCollectionInput, CreateCollectionOutput, ICreateCollectionOutputPort>
    {
        private readonly CollectionsService _collectionService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCollectionUseCase(
            IUseCaseInputValidator<CreateCollectionInput> inputValidator,
            ICreateCollectionOutputPort output,
            CollectionsService collectionService,
            IUnitOfWork unitOfWork)
            : base(inputValidator, output)
        {
            _collectionService = collectionService ?? throw new ArgumentNullException();
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException();
        }

        protected override async Task Handle(CreateCollectionInput input)
        {
            var owner = new Owner(input.Owner);

            var exists = await _collectionService.ExistsByOwnerAsync(owner);
            if (exists)
            {
                OutputPort.UserHasAlreadyOneCollection(owner);
                return;
            }

            var id = await _collectionService.CreateAsync(owner, input.Notes);
            var _ = await _unitOfWork.SaveAsync();

            OutputPort.Standard(new CreateCollectionOutput(id, owner));
        }
    }
}
