using System.Collections.Generic;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList
{
    public sealed class GetBrandsListOutput : IUseCaseOutput
    {
        private readonly IEnumerable<IBrand> result;

        public GetBrandsListOutput(IEnumerable<IBrand> result)
        {
            this.result = result;
        }

        public IEnumerable<IBrand> Result => result;
    }
}
