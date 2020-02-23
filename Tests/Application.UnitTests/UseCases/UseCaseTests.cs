using Moq;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class UseCaseTests</*TUseCase,*/ TInput>
        where TInput : IUseCaseInput
    {
        protected Mock<IUnitOfWork> UnitOfWork()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            return unitOfWorkMock;
        }

        protected void VerifyUnitOfWorkSaveHasBeenCalled(Mock<IUnitOfWork> unitOfWorkMock)
        {
            unitOfWorkMock.Verify(uw => uw.SaveAsync(), Times.Once);
        }
    }
}
