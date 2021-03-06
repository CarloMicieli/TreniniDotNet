﻿using AutoMapper;
using TreniniDotNet.Application.Catalog.Railways.GetRailwayBySlug;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Railways.GetRailwayBySlug
{
    public class GetRailwayBySlugHandler : UseCaseHandler<GetRailwayBySlugUseCase, GetRailwayBySlugRequest, GetRailwayBySlugInput>
    {
        public GetRailwayBySlugHandler(GetRailwayBySlugUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
