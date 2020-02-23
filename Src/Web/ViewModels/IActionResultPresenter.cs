using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.ViewModels
{
    public interface IActionResultPresenter
    {
        IActionResult ViewModel { get; }
    }
}
