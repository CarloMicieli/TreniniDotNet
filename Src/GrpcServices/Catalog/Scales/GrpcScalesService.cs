using System;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Catalog;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.GrpcServices.Catalog.Scales
{
    public sealed class GrpcScalesService : ScalesService.ScalesServiceBase
    {
        public GrpcScalesService(
            ScaleService scaleService,
            IUnitOfWork unitOfWork,
            CreateScalePresenter presenter)
        {
            Presenter = presenter ??
                        throw new ArgumentNullException(nameof(presenter));
            UseCase = new CreateScaleUseCase(presenter, scaleService, unitOfWork);
        }

        private CreateScalePresenter Presenter { get; }
        private ICreateScaleUseCase UseCase { get; }
    }
}
