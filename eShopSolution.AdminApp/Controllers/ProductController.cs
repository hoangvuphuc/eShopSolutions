using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        #region Get Product

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            var langId = HttpContext.Session.GetString(SystemConstant.AppSetings.DefaultLanguageId);

            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LangId = langId
            };

            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;
            if (TempData["Result"] != null)
            {
                ViewBag.SucessMsg = TempData["Result"];
            }
            return View(data);
        }

        #endregion

        #region Create Product

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productApiClient.CreateProduct(request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Create product success";
                return RedirectToAction("Index", "Product");
            }

            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);

            return View(request);

        }

        #endregion

        #region Update Product
        #endregion

        #region Edit Product
        #endregion
    }
}