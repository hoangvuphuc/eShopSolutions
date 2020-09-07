using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using eShopSolution.ViewModels.Catalog.ProductImages;
using eShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackendApi.Controllers
{
    //api/products
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        #region Public

        //// http://localhost:port/api/product?pageIndex=1&pageSize=10&CategoryId=1 
        //[HttpGet("{langId}")]
        //public async Task<IActionResult> GetAllPaging(string langId, [FromQuery] GetPublicProductPagingRequest request)
        //{
        //    var products = await _productService.GetAllByCategoryId(langId, request);
        //    return Ok(products);
        //}

        #endregion

        #region Manage

        // http://localhost:port/api/product/paging?pageIndex=1&pageSize=10&CategoryId=1 
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }

        // http://localhost:port/api/product/{Id}
        [HttpGet("{productId}/{langId}")]
        public async Task<IActionResult> GetById(int productId, string langId)
        {
            var product = await _productService.GetById(productId, langId);
            if (product == null)
                return BadRequest("Can't find product");
            return Ok(product);
        }

        // http://localhost:port/api/product
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();

            //we can return the product by add a query
            //var product = await _manageProductService.GetById(productId);
            //and ese CreatedAtAction moethod
            //return CreatedAtAction(nameof(GetById), new { id = productId }, product);

            return Created(nameof(GetById), new { Id = productId, langId = request.LanguageId });
        }


        // http://localhost:port/api/product
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var affectedResult = await _productService.Update(request);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        // http://localhost:port/api/product/{Id}
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResult = await _productService.Delete(productId);
            if (affectedResult == 0)
                return BadRequest();

            return Ok();
        }

        // http://localhost:port/api/product/price/{productId}/{newPrice}
        [HttpPatch("price/{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _productService.UpdatePrice(productId, newPrice);
            if (isSuccessful == false)
                return BadRequest();

            return Ok();
        }

        // http://localhost:port/api/product/stock/{id}/{addedQty}
        [HttpPut("stock/{id}/{addedQty}")]
        public async Task<IActionResult> UpdateStock(int id, int addedQty)
        {
            var isSuccessful = await _productService.UpdateStock(id, addedQty);
            if (isSuccessful == false)
                return BadRequest();

            return Ok();
        }

        // http://localhost:port/api/product/viewcount/{id}
        [HttpPut("viewcount/{id}")]
        public async Task<IActionResult> AddViewCount(int id)
        {
            await _productService.AddViewCount(id);

            return Ok();
        }


        #endregion

        #region Image

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();

            var image = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId}, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _productService.DeleteImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        // http://localhost:port/api/{Id}
        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Can't find image");
            return Ok(image);
        }

        #endregion

        #region CategoryAssign

        //PUT  http://localhost:port/api/user/id
        [HttpPut("{productId}/categories")]
        public async Task<IActionResult> CategoryAssign(int productId, [FromBody] CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.CategoryAssign(productId, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        #endregion


    }
}
