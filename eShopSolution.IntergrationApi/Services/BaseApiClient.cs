using eShopSolution.Utilities.Constants;
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

namespace eShopSolution.IntergrationApi.Services
{
    public class BaseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiClient(IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var token = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstant.AppSetings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSetings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializeObject = (TResponse)JsonConvert.DeserializeObject(result, typeof(TResponse));
                return myDeserializeObject;
            }

            return JsonConvert.DeserializeObject<TResponse>(result);
        }

        protected async Task<List<TResponse>> GetListAsync<TResponse>(string url)
        {
            var token = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstant.AppSetings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSetings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<TResponse> myDeserializeObject = (List<TResponse>)JsonConvert.DeserializeObject(result, typeof(List<TResponse>));
                return myDeserializeObject;
            }

            //return JsonConvert.DeserializeObject<List<TResponse>>(result);
            throw new Exception(result);
        }

        protected async Task<TResponse> PostAsync<TResponse>(string url)
        {
            var token = _httpContextAccessor
                .HttpContext
                .Session
                .GetString(SystemConstant.AppSetings.Token);

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSetings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializeObject = (TResponse)JsonConvert.DeserializeObject(result, typeof(TResponse));
                return myDeserializeObject;
            }

            return JsonConvert.DeserializeObject<TResponse>(result);
        }

    }
}
