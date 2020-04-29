using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.CreateShop
{
    public sealed class CreateShopHandler : UseCaseHandler<ICreateShopUseCase, CreateShopRequest, CreateShopInput>
    {
        public CreateShopHandler(ICreateShopUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
