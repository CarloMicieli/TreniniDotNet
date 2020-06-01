using TreniniDotNet.Domain.Catalog.ValueObjects;
using TreniniDotNet.Domain.Catalog.Railways;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestHelpers")]
namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public sealed class RollingStock : IEquatable<RollingStock>, IRollingStock
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
        {
            RollingStockId = rollingStockId;
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
        public RollingStockId RollingStockId { get; }

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

        #region [ Equality ]

        public override bool Equals(object obj)
        {
            if (obj is RollingStock other)
            {
                return AreEquals(this, other);
            }

            return false;
        }

        public bool Equals(RollingStock other) => AreEquals(this, other);

        private static bool AreEquals(RollingStock left, RollingStock right) =>
            left.RollingStockId == right.RollingStockId;

        #endregion

        public override int GetHashCode() => RollingStockId.GetHashCode();

        public override string ToString()
        {
            return $"RollingStock({RollingStockId} {Epoch} {Railway.Name} {Category})";
        }
    }
}
