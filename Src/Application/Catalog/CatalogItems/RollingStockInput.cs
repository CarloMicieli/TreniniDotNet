using TreniniDotNet.Common.UseCases.Interfaces.Input;

namespace TreniniDotNet.Application.Catalog.CatalogItems
{
    public sealed class RollingStockInput : IUseCaseInput
    {
        public RollingStockInput(
            string epoch, string category,
            string railway,
            string? className, string? roadNumber,
            string? typeName, string? passengerCarType, string? serviceLevel,
            LengthOverBufferInput? lengthOverBuffer,
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
            LengthOverBuffer = lengthOverBuffer ?? LengthOverBufferInput.Default();
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

        public LengthOverBufferInput LengthOverBuffer { get; }

        public string? DccInterface { get; }

        public string? Control { get; }
    }
}
