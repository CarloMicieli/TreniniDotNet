namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct RoadNumber
    {
        private readonly string _value;

        public RoadNumber(string value)
        {
            _value = value;
        }
    }
}
