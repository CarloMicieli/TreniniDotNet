namespace TreniniDotNet.Application.Catalog.CatalogItems
{
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
