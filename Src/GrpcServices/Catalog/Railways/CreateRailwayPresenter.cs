using System;
using TreniniDotNet.Application.Catalog.Railways.CreateRailway;
using TreniniDotNet.Common;
using TreniniDotNet.GrpcServices.Infrastructure;

namespace TreniniDotNet.GrpcServices.Catalog.Railways
{
    public sealed class CreateRailwayPresenter : DefaultGrpcPresenter<CreateRailwayOutput, object>, ICreateRailwayOutputPort
    {
        public CreateRailwayPresenter()
            : base(Mapping)
        {
        }

        private static object Mapping(CreateRailwayOutput output) => throw new NotImplementedException();

        public void RailwayAlreadyExists(Slug railway)
        {
            throw new NotImplementedException();
        }
    }
}
