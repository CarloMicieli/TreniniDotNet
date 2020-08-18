using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.Infrastructure.ViewModels
{
    public interface IActionResultPresenter
    {
        IActionResult ViewModel { get; }
    }
}