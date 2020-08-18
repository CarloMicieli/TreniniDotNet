using AutoMapper;
using TreniniDotNet.Application.Collecting.Shops.CreateShop;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Shops.CreateShop
{
    public sealed class CreateShopHandler : UseCaseHandler<CreateShopUseCase, CreateShopRequest, CreateShopInput>
    {
        public CreateShopHandler(CreateShopUseCase useCase, IMapper mapper)
            : base(useCase, mapper)
        {
        }
    }
}
