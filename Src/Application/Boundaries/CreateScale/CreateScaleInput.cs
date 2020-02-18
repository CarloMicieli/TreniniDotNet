using TreniniDotNet.Common.Interfaces;
using TreniniDotNet.Domain.Catalog.Scales;

namespace TreniniDotNet.Application.Boundaries.CreateScale
{
    public class CreateScaleInput : IUseCaseInput
    {
        private readonly Scale _scale;

        public CreateScaleInput(Scale scale)
        {
            _scale = scale;
        }

        public Scale Scale => _scale;
    }
}