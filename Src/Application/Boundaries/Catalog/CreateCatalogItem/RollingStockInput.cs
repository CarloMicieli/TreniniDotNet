namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class RollingStockInput
    {
        public RollingStockInput(
            string era, string category,
            string railway,
            string? className, string? roadNumber, string? typeName,
            decimal? length,
            string? control, string? dccInterface)
        {
            Era = era;
            Category = category;
            Railway = railway;
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
            Length = length;
            Control = control;
            DccInterface = dccInterface;
        }

        public string Era { get; }

        public string Category { get; }

        public string Railway { get; }

        public string? ClassName { get; }

        public string? TypeName { get; }

        public string? RoadNumber { get; }

        public decimal? Length { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }
}