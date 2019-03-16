﻿using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountsViewModel;
namespace WishList.Controllers
{
    [Authorize]
    public class AccountController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost,AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
              var result = _userManager.CreateAsync(new ApplicationUser() { UserName = model.Email, Email = model.Email }, model.Password).Result;
              if(!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return View("Register", model);
                }
                return  RedirectToAction("HomeController.Index");
            }
            else
            {
                return View("Register", model);
            }
           
        }
    }
}