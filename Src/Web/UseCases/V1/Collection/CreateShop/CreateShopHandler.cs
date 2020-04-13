using AutoMapper;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;

namespace TreniniDotNet.Web.UseCases.V1.Collection.CreateShop
{
    public sealed class CreateShopHandler : UseCaseHandler<ICreateShopUseCase, CreateShopRequest, CreateShopInput>
    {
        public CreateShopHandler(ICreateShopUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
