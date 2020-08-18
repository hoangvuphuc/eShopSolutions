using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace eShopSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is home page of eShop." },
                new AppConfig() { Key = "HomeKeyword", Value = "This is key word of eShop." },
                new AppConfig() { Key = "HomeDescription", Value = "This is description of eShop." }
                );

            modelBuilder.Entity<Language>().HasData(
                new Language() { 
                    Id ="vi-VN", 
                    Name = "Tieng Viet", 
                    IsDefault = true 
                },
                new Language() { 
                    Id = "en-US", 
                    Name = "English", 
                    IsDefault = false 
                }
                );

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true, 
                    ParentId = null, 
                    SortOrder = 1, 
                    Status = Enums.Status.Active
                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Enums.Status.Active
                }
                );

            modelBuilder.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id= 1,
                    CategoryId = 1,
                    Name = "Ao nam",
                    LanguageId = "vi-VN",
                    SeoDescription = "San pham ao thoi trang nam",
                    SeoTitle = "San pham ao thoi trang nam",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en-US",
                    SeoDescription = "The T-shirt products for men",
                    SeoTitle = "The T-shirt products for men",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Ao nu",
                    LanguageId = "vi-VN",
                    SeoDescription = "San pham ao thoi trang nu",
                    SeoTitle = "San pham ao thoi trang nu",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en-US",
                    SeoDescription = "The T-shirt products for women",
                    SeoTitle = "The T-shirt products for women",
                    SeoAlias = "San pham ao thoi trang nam"
                }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now, 
                    OriginalPrice = 100000, 
                    Price = 200000, 
                    Stocks = 0, 
                    ViewCount = 100
                }
                );

            modelBuilder.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id= 1,
                    ProductId = 1,
                    Name = "Ao so mi nam Viet Tien",
                    LanguageId = "vi-VN",
                    SeoAlias = "Ao-so-mi-nam-viet-tien",
                    SeoDescription = "Ao so mi nam Viet Tien",
                    SeoTitle = "Ao so mi nam Viet Tien",
                    Details = "Ao so mi nam Viet Tien",
                    Description = "Ao so mi nam Viet Tien"
                },
                new ProductTranslation()
                {
                    Id=2,
                    ProductId = 1,
                    Name = "Viet Tien Men T-Shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-T-shirt",
                    SeoDescription = "TViet Tien Men T-Shirt",
                    SeoTitle = "Viet Tien Men T-Shirt",
                    Details = "Viet Tien Men T-Shirt",
                    Description = "Viet Tien Men T-Shirt"
                }
                );

            modelBuilder.Entity<ProductInCategory>().HasData(
                new ProductInCategory()
                {
                    ProductId = 1,
                    CategoryId = 1
                }
                );
        }
    }
}
