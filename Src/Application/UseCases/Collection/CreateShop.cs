using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Collection.CreateShop;

namespace TreniniDotNet.Application.UseCases.Collection
{
    public sealed class CreateShop : ValidatedUseCase<CreateShopInput, ICreateShopOutputPort>, ICreateShopUseCase
    {
        public CreateShop(ICreateShopOutputPort output)
            : base(new CreateShopInputValidator(), output)
        {
        }

        protected override Task Handle(CreateShopInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
