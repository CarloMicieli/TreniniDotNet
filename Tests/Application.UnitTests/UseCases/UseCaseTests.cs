using FluentValidation;
using Moq;
using System.Collections.Generic;
using TreniniDotNet.Application.Services;
using TreniniDotNet.Common.Interfaces;

namespace TreniniDotNet.Application.UseCases
{
    public abstract class UseCaseTests</*TUseCase,*/ TInput, TValidator>
        where TInput : IUseCaseInput
        where TValidator : AbstractValidator<TInput>, new()
        //where TUseCase : class, IUseCase<TInput>
    {





        //protected TUseCase SetupUseCase()
        //{


        //    return null;
        //}


        protected Mock<IUnitOfWork> UnitOfWork()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            return unitOfWorkMock;
        }

        protected void VerifyUnitOfWorkSaveHasBeenCalled(Mock<IUnitOfWork> unitOfWorkMock)
        {
            unitOfWorkMock.Verify(uw => uw.SaveAsync(), Times.Once);
        }

        protected IUseCaseInputValidator<TInput> NewValidator()
        {
            return new UseCaseInputValidator<TInput>(
                new List<IValidator<TInput>> { new TValidator() });
        }
    }
}
