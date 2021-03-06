﻿using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.EditRailway;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.EditRailway
{
    public sealed class EditRailwayHandler : UseCaseHandler<EditRailwayUseCase, EditRailwayRequest, EditRailwayInput>
    {
        public EditRailwayHandler(EditRailwayUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
