using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace eShopSolution.AdminApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IRoleApiClient _roleApiClient;
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IRoleApiClient roleApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _roleApiClient = roleApiClient;
            _configuration = configuration;
        }

        #region Get User

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 2)
        {
            var request =  new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
            ViewBag.Keyword = keyword;
            if(TempData["Result"] != null)
            {
                ViewBag.SucessMsg = TempData["Result"];
            }
            return View(data.ResultObj);
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            return View(result.ResultObj);
        }

        #endregion

        #region Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");

            return RedirectToAction("Index", "Login");
            
        }
        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.RegisterUser(request);
            if(result.IsSuccessed)
            {
                TempData["result"] = "Create user success";
                return RedirectToAction("Index", "User");
            }

            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);

            return View(request);
            
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if(result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UpdateRequest()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Dob = user.Dob
                };
                return View(updateRequest);

            }
            return RedirectToAction("Error", "Home");
            
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.Update(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Edit user success";
                return RedirectToAction("Index", "User");
            }
            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);
            return View(request);

        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var deleteRequest = new DeleteRequest()
                {
                    Id = id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserName = user.UserName,
                    Email = user.Email
                };
                return View(deleteRequest);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.Delete(request.Id);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Delete user success";
                return RedirectToAction("Index", "User");
            }
            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);
            return View(request);

        }

        #endregion 

        #region RoleAssign
        [HttpGet]
        public async Task<IActionResult> RoleAssign(Guid id)
        {
            var roleAssignRequest = await GetRoleAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> RoleAssign(RoleAssignRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _userApiClient.RoleAssign(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Assign role success";
                return RedirectToAction("Index", "User");
            }
            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);
            var roleAssignRequest = await GetRoleAssignRequest(request.Id);
            return View(roleAssignRequest);

        }
        #endregion

        #region Common

        private async Task<RoleAssignRequest> GetRoleAssignRequest(Guid id)
        {
            var userResult = await _userApiClient.GetById(id);
            var roleResult = await _roleApiClient.GetAll();

            var roleAssignRequest = new RoleAssignRequest();
            if (userResult.IsSuccessed)
            {
                var user = userResult.ResultObj;
                var roles = roleResult.ResultObj;


                foreach (var role in roles)
                {
                    roleAssignRequest.Roles.Add(new SelectItem()
                    {
                        Id = role.Id.ToString(),
                        Name = role.Name,
                        Selected = user.Roles.Contains(role.Name)
                    });
                }

            } 
            
            return roleAssignRequest;
        }

        #endregion

    }
}
