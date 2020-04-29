using TreniniDotNet.Application.Catalog.Brands.GetBrandsList;
using TreniniDotNet.TestHelpers.InMemory.OutputPorts;

namespace TreniniDotNet.Application.InMemory.Catalog.Brands.OutputPorts
{
    public sealed class GetBrandsListOutputPort : OutputPortTestHelper<GetBrandsListOutput>, IGetBrandsListOutputPort
    {
    }
}
