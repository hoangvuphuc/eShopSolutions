using eShopSolution.Utilities.Constants;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public ProductApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor) : base(httpClientFactory, configuration, httpContextAccessor)
        {
            //Inherit from BaseApiClient
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<PageResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request)
        {
            var result = await base.GetAsync<PageResult<ProductViewModel>>($"/api/products/paging?pageIndex=" +
                $"{request.PageIndex}&PageSize={request.PageSize}&keyword={request.Keyword}&langId={request.LangId}");

            return result;
        }

        public async Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request)
        {
            var token = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstant.AppSetings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSetings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var langId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSetings.DefaultLanguageId);
            //Convert ProductCreateRequest to MultipartFormDataContent to except binary

            var requestContent = new MultipartFormDataContent();

            if(request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "ThumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stocks.ToString()), "stocks");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");
            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(langId), "languageId");

            var response = await client.PostAsync($"/api/products/", requestContent);

            if(response.IsSuccessStatusCode)
            {
                return new ApiSuccessResult<bool>();
            }
            
            return new ApiErrorResult<bool>(response.ToString());

        }
    }
}
