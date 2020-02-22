using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public sealed class CreateScaleInput : IUseCaseInput
    {
        private readonly string? _name;
        private readonly decimal? _ratio;
        private readonly decimal? _gauge;
        private readonly string? _trackGauge;
        private readonly string? _notes;

        public CreateScaleInput(string? name, decimal? ratio, decimal? gauge, string? trackGauge, string? notes)
        {
            _name = name;
            _ratio = ratio;
            _gauge = gauge;
            _trackGauge = trackGauge;
            _notes = notes;
        }

        public string? Name => _name;

        public decimal? Ratio => _ratio;

        public decimal? Gauge => _gauge;

        public string? TrackGauge => _trackGauge;

        public string? Notes => _notes;
    }
}