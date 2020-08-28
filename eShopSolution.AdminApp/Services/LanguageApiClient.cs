using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Languages;
using eShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        //private readonly IHttpClientFactory _httpClientFactory;
        //private readonly IConfiguration _configuration;
        //private readonly IHttpContextAccessor _httpContextAccessor;

        //public LanguageApiClient(IHttpClientFactory httpClientFactory,
        //    IConfiguration configuration,
        //    IHttpContextAccessor httpContextAccessor)
        //{
        //    _httpClientFactory = httpClientFactory;
        //    _configuration = configuration;
        //    _httpContextAccessor = httpContextAccessor;
        //}

        
        public LanguageApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
            : base (httpClientFactory, configuration, httpContextAccessor)
        {
            //Change to inherit from BaseApiClient
        }

        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            //var token = _httpContextAccessor.HttpContext.Session.GetString("Token");
            //var client = _httpClientFactory.CreateClient();
            //client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //var response = await client.GetAsync($"/api/languages");
            //var result = await response.Content.ReadAsStringAsync();
            //if (response.IsSuccessStatusCode)
            //{
            //    List<LanguageViewModel> resultList = (List<LanguageViewModel>)JsonConvert.DeserializeObject(result, typeof(List<LanguageViewModel>));
            //    return new ApiSuccessResult<List<LanguageViewModel>>(resultList);
            //}

            //return JsonConvert.DeserializeObject<ApiErrorResult<List<LanguageViewModel>>>(result);

            //Convert to use BaseApiClient
            return await GetAsync<ApiResult<List<LanguageViewModel>>>("/api/languages");
        }
    }
}
