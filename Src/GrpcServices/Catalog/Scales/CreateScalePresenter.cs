using System;
using TreniniDotNet.Application.Catalog.Scales.CreateScale;
using TreniniDotNet.Common;
using TreniniDotNet.GrpcServices.Infrastructure;

namespace TreniniDotNet.GrpcServices.Catalog.Scales
{
    public sealed class CreateScalePresenter : DefaultGrpcPresenter<CreateScaleOutput, object>, ICreateScaleOutputPort
    {
        public CreateScalePresenter()
            : base(Mapping)
        {
        }

        private static object Mapping(CreateScaleOutput output) => throw new NotImplementedException();

        public void ScaleAlreadyExists(Slug scaleSlug)
        {
            throw new NotImplementedException();
        }
    }
}
