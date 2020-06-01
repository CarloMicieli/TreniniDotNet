using System;
using TreniniDotNet.Common;
using TreniniDotNet.Common.UseCases.Interfaces.Input;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class EditRollingStockInput : IUseCaseInput
    {
        public EditRollingStockInput(Slug slug,
            RollingStockId rollingStockId,
            string? epoch, string? category,
            string? railway,
            string? className, string? roadNumber,
            string? typeName, string? passengerCarType, string? serviceLevel,
            string? couplers,
            string? livery,
            LengthOverBufferInput? lengthOverBuffer,
            decimal? minRadius,
            string? control, string? dccInterface)
        {
            RollingStockId = rollingStockId;
            Slug = slug;
            Values = new RollingStockModifiedValues(
                epoch, category,
                railway,
                className, roadNumber, typeName,
                couplers,
                livery,
                passengerCarType, serviceLevel,
                lengthOverBuffer,
                minRadius,
                control, dccInterface);
        }

        public RollingStockId RollingStockId { get; }
        public Slug Slug { get; }
        public RollingStockModifiedValues Values { get; }
    }

    public sealed class RollingStockModifiedValues
    {
        public RollingStockModifiedValues(
            string? epoch, string? category,
            string? railway,
            string? className, string? roadNumber, string? typeName,
            string? couplers,
            string? livery, string? passengerCarType, string? serviceLevel,
            LengthOverBufferInput? lengthOverBuffer,
            decimal? minRadius,
            string? control, string? dccInterface)
        {
            Epoch = epoch;
            Category = category;
            Railway = railway;
            ClassName = className;
            RoadNumber = roadNumber;
            Couplers = couplers;
            Livery = livery;
            TypeName = typeName;
            PassengerCarType = passengerCarType;
            ServiceLevel = serviceLevel;
            LengthOverBuffer = lengthOverBuffer;
            MinRadius = minRadius;
            Control = control;
            DccInterface = dccInterface;
        }

        public string? Epoch { get; }

        public string? Category { get; }

        public string? Railway { get; }

        public string? ClassName { get; }

        public string? TypeName { get; }

        public string? RoadNumber { get; }

        public string? Couplers { get; }

        public string? Livery { get; }

        public string? PassengerCarType { get; }

        public string? ServiceLevel { get; }

        public LengthOverBufferInput? LengthOverBuffer { get; }

        public decimal? MinRadius { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }
}
