using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.Catalog.CreateScale
{
    public class CreateScaleInput : IUseCaseInput
    {
        private readonly IScale _scale;

        public CreateScaleInput(IScale scale)
        {
            _scale = scale;
        }

        public IScale Scale => _scale;
    }
}