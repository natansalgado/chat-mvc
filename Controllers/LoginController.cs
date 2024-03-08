using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using mvc.Dtos;
using mvc.Models;
using mvc.Services.Interfaces;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IPasswordHashService _passwordHashService;

        public LoginController(IUserService userService, ITokenService tokenService, IPasswordHashService passwordHashService)
        {
            _userService = userService;
            _tokenService = tokenService;
            _passwordHashService = passwordHashService;
        }

        public ActionResult Index()
        {
            string userToken = HttpContext.Session.GetString("UserToken");

            if (!string.IsNullOrEmpty(userToken))
            {
                return RedirectToAction("Index", "Chat");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginDto loginDto)
        {
            UserModel userModel = _userService.GetByUserName(loginDto.UserName);

            if (userModel == null || !_passwordHashService.VerifyPassword(loginDto.Password, userModel.Password))
            {
                ViewBag.ErrorMessage = "Usuário e senha não correspondem";
                return View();
            }

            userModel.Password = null;

            string token = _tokenService.GenerateToken(userModel);

            HttpContext.Session.SetString("UserToken", token);
            HttpContext.Session.SetObject("UserModel", userModel);

            return RedirectToAction("Index", "Chat");
        }
    }
}