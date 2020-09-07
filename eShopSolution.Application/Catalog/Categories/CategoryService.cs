using eShopSolution.Application.Common;
using eShopSolution.Data.EF;
using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace eShopSolution.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        #region variable and constructor

        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;

        public CategoryService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        #endregion

        #region Category
        public async Task<List<CategoryViewModel>> GetAll(string langId)
        {
            //1. Select join
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == langId
                        select new { c, ct };

            //2. where
            return await query.Select(x => new CategoryViewModel()
            {
                Id = x.c.Id,
                //SortOrder =x.c.SortOrder,
                Name = x.ct.Name
                //SeoDescription = x.ct.SeoDescription,
                //SeoTitle = x.ct.SeoTitle,
                //SeoAlias = x.ct.SeoAlias
            }).ToListAsync();
        }

        #endregion 
    }
}
