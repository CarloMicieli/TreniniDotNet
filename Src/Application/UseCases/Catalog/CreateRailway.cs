﻿using System;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.CreateRailway;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class CreateRailway : ValidatedUseCase<CreateRailwayInput, ICreateRailwayOutputPort>, ICreateRailwayUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RailwayService _railwayService;

        public CreateRailway(
            ICreateRailwayOutputPort outputPort,
            RailwayService railwayService,
            IUnitOfWork unitOfWork)
            : base(new CreateRailwayInputValidator(), outputPort)
        {
            _unitOfWork = unitOfWork;
            _railwayService = railwayService;
        }

        protected override async Task Handle(CreateRailwayInput input)
        {
            var railwayName = input.Name!;
            var slug = Slug.Of(railwayName);

            bool exists = await _railwayService.RailwayAlreadyExists(slug);
            if (exists)
            {
                OutputPort.RailwayAlreadyExists($"Railway '{railwayName}' already exists");
                return;
            }

            var status = input.Status ?? RailwayStatus.Active.ToString();

            var _ = await _railwayService.CreateRailway(
                railwayName,
                slug,
                input.CompanyName,
                input.Country,
                input.OperatingSince,
                input.OperatingUntil,
                status.ToRailwayStatus() ?? RailwayStatus.Active); //TODO: fixme

            await _unitOfWork.SaveAsync();

            BuildOutput(slug);
        }

        private void BuildOutput(Slug slug)
        {
            var output = new CreateRailwayOutput(slug);
            OutputPort.Standard(output);
        }
    }
}
