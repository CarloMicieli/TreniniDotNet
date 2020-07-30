namespace TreniniDotNet.Application.Catalog.CatalogItems.EditRollingStock
{
    public sealed class RollingStockModifiedValues
    {
        public RollingStockModifiedValues(
            string? epoch, string? category,
            string? railway,
            string? className, string? roadNumber, string? typeName, string? series,
            string? couplers,
            string? livery, string? depot,
            string? passengerCarType, string? serviceLevel,
            LengthOverBufferInput? lengthOverBuffer,
            decimal? minRadius,
            string? control, string? dccInterface)
        {
            Epoch = epoch;
            Category = category;
            Railway = railway;
            ClassName = className;
            RoadNumber = roadNumber;
            Series = series;
            Couplers = couplers;
            Livery = livery;
            Depot = depot;
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

        public string? Series { get; }

        public string? Couplers { get; }

        public string? Livery { get; }

        public string? Depot { get; }

        public string? PassengerCarType { get; }

        public string? ServiceLevel { get; }

        public LengthOverBufferInput? LengthOverBuffer { get; }

        public decimal? MinRadius { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }
}