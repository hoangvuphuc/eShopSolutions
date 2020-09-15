using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.IntergrationApi.Services;
using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace eShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
        }

        #region Get Product

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 2)
        {
            var langId = HttpContext.Session.GetString(SystemConstant.AppSetings.DefaultLanguageId);

            var request = new GetManageProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LangId = langId,
                CategoryId = categoryId
            };

            var data = await _productApiClient.GetPagings(request);
            ViewBag.Keyword = keyword;

            var categories = await _categoryApiClient.GetAll(langId);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });

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

        #region Assign Category

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var categoryAssignRequest = await GetCategoryAssignRequest(id);
            return View(categoryAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productApiClient.CategoryAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Assign category success";
                return RedirectToAction("Index", "Product");
            }
            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);
            var categoryAssignRequest = await GetCategoryAssignRequest(request.Id);
            return View(categoryAssignRequest);

        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var langId = HttpContext.Session.GetString(SystemConstant.AppSetings.DefaultLanguageId);

            var productResult = await _productApiClient.GetById(id, langId);
            var categoryResult = await _categoryApiClient.GetAll(langId);
            var categoryAssignRequest = new CategoryAssignRequest();
            
            var product = productResult;
            var categories = categoryResult;


            foreach (var category in categories)
            {
                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Selected = product.Categories.Contains(category.Id)
                });
            }

            return categoryAssignRequest;
        }
        #endregion
    }
}