namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    public readonly struct RoadName
    {
        private readonly string _value;

        public RoadName(string value)
        {
            _value = value;
        }
    }
}
