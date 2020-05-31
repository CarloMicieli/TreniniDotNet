using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Catalog;
using TreniniDotNet.Common;
using TreniniDotNet.GrpcServices.Infrastructure;

namespace TreniniDotNet.GrpcServices.Catalog.Scales
{
    public sealed class CreateScalePresenter : DefaultGrpcPresenter<CreateScaleOutput, CreateScaleResponse>, ICreateScaleOutputPort
    {
        public CreateScalePresenter()
            : base(Mapping)
        {
        }

        private static CreateScaleResponse Mapping(CreateScaleOutput output) => new CreateScaleResponse { Slug = output.Slug };

        public void ScaleAlreadyExists(Slug scaleSlug)
        {
            AlreadyExists(scaleSlug.ToString());
        }
    }
}
