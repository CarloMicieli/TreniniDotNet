using System;
using System.Collections.Generic;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.CatalogItems;
using TreniniDotNet.Domain.Catalog.Railways;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class RollingStockInput
    {
        public RollingStockInput(
            string epoch, string category,
            string railway,
            string? className, string? roadNumber,
            string? typeName, string? passengerCarType, string? serviceLevel,
            LengthOverBufferInput? length,
            string? control, string? dccInterface)
        {
            Epoch = epoch;
            Category = category;
            Railway = railway;
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
            PassengerCarType = passengerCarType;
            ServiceLevel = serviceLevel;
            Length = length ?? LengthOverBufferInput.Default();
            Control = control;
            DccInterface = dccInterface;
        }

        public string Epoch { get; }

        public string Category { get; }

        public string Railway { get; }

        public string? ClassName { get; }

        public string? TypeName { get; }

        public string? RoadNumber { get; }

        public string? PassengerCarType { get; }

        public string? ServiceLevel { get; }

        public LengthOverBufferInput Length { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }
}
