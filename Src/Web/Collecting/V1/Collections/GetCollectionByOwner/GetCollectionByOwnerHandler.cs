﻿using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionByOwner
{
    public sealed class GetCollectionByOwnerHandler : UseCaseHandler<GetCollectionByOwnerUseCase, GetCollectionByOwnerRequest, GetCollectionByOwnerInput>
    {
        public GetCollectionByOwnerHandler(GetCollectionByOwnerUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
