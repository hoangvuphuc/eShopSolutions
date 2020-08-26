﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using eShopSolution.AdminApp.Services;
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
        private readonly IConfiguration _configuration;

        public UserController(IUserApiClient userApiClient, IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        #region Get User

        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 1)
        {
            var request =  new GetUserPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await _userApiClient.GetUsersPagings(request);
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
                    Id = user.Id,
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
                return RedirectToAction("Index", "User");
            }
            //Add custome message to ModelState
            ModelState.AddModelError("", result.Message);
            return View(request);

        }
        #endregion

        #region
        #endregion 


    }
}
