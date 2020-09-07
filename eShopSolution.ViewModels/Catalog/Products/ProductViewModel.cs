using eShopSolution.ViewModels.Catalog.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Catalog.Products
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Stocks { get; set; }
        public int ViewCount { get; set; }
        public DateTime DateCreated { get; set; }

        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }

        public List<int> Categories  { set; get; }
}
}
