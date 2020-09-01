﻿using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PageResult<ProductViewModel>> GetPagings(GetManageProductPagingRequest request);

        Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request);
    }
}
