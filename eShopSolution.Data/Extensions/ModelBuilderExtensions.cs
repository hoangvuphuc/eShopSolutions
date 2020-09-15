using eShopSolution.Data.Entities;
using Microsoft.AspNetCore.Identity;
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
                    Id ="vi", 
                    Name = "Tieng Viet", 
                    IsDefault = true 
                },
                new Language() { 
                    Id = "en", 
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
                    LanguageId = "vi",
                    SeoDescription = "San pham ao thoi trang nam",
                    SeoTitle = "San pham ao thoi trang nam",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men Shirt",
                    LanguageId = "en",
                    SeoDescription = "The T-shirt products for men",
                    SeoTitle = "The T-shirt products for men",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Ao nu",
                    LanguageId = "vi",
                    SeoDescription = "San pham ao thoi trang nu",
                    SeoTitle = "San pham ao thoi trang nu",
                    SeoAlias = "San pham ao thoi trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women Shirt",
                    LanguageId = "en",
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
                    LanguageId = "vi",
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
                    LanguageId = "en",
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

            //Identity for admin 
            // any guid
            var roleId = new Guid("0C13D433-42E4-4162-8A8F-0F2B348D72DC");
            var adminId = new Guid("5DC2BA10-4647-4476-97C0-D5CFCEE1B1F5");
            modelBuilder.Entity<AppRole>().HasData(new AppRole
            {
                Id = roleId,
                Name = "admin",
                NormalizedName = "admin",
                Description = "Administrator role"
            });

            var hasher = new PasswordHasher<AppUser>();
            modelBuilder.Entity<AppUser>().HasData(new AppUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "eshop.admin@gmail.com",
                NormalizedEmail = "eshop.admin@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Abc12356$"),
                SecurityStamp = string.Empty,
                FirstName = "Admin",
                LastName = "User",
                Dob = new DateTime(2020, 05, 20)
            }); ;

            modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = roleId,
                UserId = adminId
            });
        }
    }
}
