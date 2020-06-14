using System;
using System.Runtime.CompilerServices;
using TreniniDotNet.Common;
using TreniniDotNet.Domain.Catalog.Railways;
using TreniniDotNet.Domain.Catalog.ValueObjects;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStock : Entity<RollingStockId>, IRollingStock
    {
        internal RollingStock(
            RollingStockId rollingStockId,
            IRailwayInfo railway,
            Category category,
            Epoch epoch,
            LengthOverBuffer? length,
            MinRadius? minRadius,
            Prototype? prototype,
            Couplers? couplers,
            string? livery,
            string? depot,
            PassengerCarType? passengerCarType, ServiceLevel? serviceLevel,
            DccInterface dccInterface, Control control)
            : base(rollingStockId)
        {
            Railway = railway;
            Category = category;
            Epoch = epoch;
            Length = length;
            MinRadius = minRadius;
            Prototype = prototype;
            Couplers = couplers;
            Livery = livery;
            Depot = depot;
            ServiceLevel = serviceLevel;
            PassengerCarType = passengerCarType;
            DccInterface = dccInterface;
            Control = control;
        }

        #region [ Properties ]
        public IRailwayInfo Railway { get; }

        public Category Category { get; }

        public Epoch Epoch { get; }

        public LengthOverBuffer? Length { get; }

        public MinRadius? MinRadius { get; }

        public Prototype? Prototype { get; }

        public Couplers? Couplers { get; }

        public string? Livery { get; }

        public string? Depot { get; }

        public PassengerCarType? PassengerCarType { get; }

        public ServiceLevel? ServiceLevel { get; }

        public Control Control { get; }

        public DccInterface DccInterface { get; }
        #endregion

        public override string ToString()
        {
            return $"RollingStock({Id} {Epoch} {Railway.Name} {Category})";
        }
    }
}
