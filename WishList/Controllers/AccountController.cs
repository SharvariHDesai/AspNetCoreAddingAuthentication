﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WishList.Models;
using WishList.Models.AccountViewModels;
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
        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View("Register");
        }
        [HttpPost,AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
             return View("Register", model);
              var result = _userManager.CreateAsync(new ApplicationUser() { UserName = model.Email, Email = model.Email }, model.Password).Result;
              if(!result.Succeeded)
                {
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("Password", error.Description);
                    }
                    return View("Register", model);
                }
            return RedirectToAction("Index","Home");
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Login", model);
            var result = _signInManager.PasswordSignInAsync(new ApplicationUser() { UserName = model.Email, Email = model.Email }, model.Password, false, false).Result;
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
            return RedirectToAction("Index", "Item");
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
    }

