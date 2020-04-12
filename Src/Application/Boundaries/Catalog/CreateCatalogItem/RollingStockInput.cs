namespace TreniniDotNet.Application.Boundaries.Catalog.CreateCatalogItem
{
    public sealed class RollingStockInput
    {
        public RollingStockInput(
            string era, string category,
            string railway,
            string? className, string? roadNumber, string? typeName,
            LengthOverBufferInput? length,
            string? control, string? dccInterface)
        {
            Era = era;
            Category = category;
            Railway = railway;
            ClassName = className;
            RoadNumber = roadNumber;
            TypeName = typeName;
            Length = length ?? LengthOverBufferInput.Default();
            Control = control;
            DccInterface = dccInterface;
        }

        public string Era { get; }

        public string Category { get; }

        public string Railway { get; }

        public string? ClassName { get; }

        public string? TypeName { get; }

        public string? RoadNumber { get; }

        public LengthOverBufferInput Length { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }

    public sealed class LengthOverBufferInput
    {
        public LengthOverBufferInput(decimal? millimeters, decimal? inches)
        {
            Millimeters = millimeters;
            Inches = inches;
        }

        public decimal? Millimeters { get; }
        public decimal? Inches { get; }

        public void Deconstruct(out decimal? mm, out decimal? inches)
        {
            mm = Millimeters;
            inches = Inches;
        }

        public static LengthOverBufferInput Default() =>
            new LengthOverBufferInput(null, null);
    }
}