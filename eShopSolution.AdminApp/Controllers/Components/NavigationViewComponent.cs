using eShopSolution.AdminApp.Models;
using eShopSolution.IntergrationApi.Services;
using eShopSolution.Utilities.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;

        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var navigationViewModel = new NavigationViewModel()
            {
                CurrentLangId = HttpContext.Session.GetString(SystemConstant.AppSetings.DefaultLanguageId),
                Languages = languages.ResultObj
            };
            return View("Default", navigationViewModel);
        }
    }
}
