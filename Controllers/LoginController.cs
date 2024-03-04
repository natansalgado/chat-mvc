using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Dtos;
using mvc.Models;
using mvc.Services;
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
            UserModel user = _userService.GetByUserName(loginDto.UserName);

            if (user == null || !_passwordHashService.VerifyPassword(loginDto.Password, user.Password))
            {
                ViewBag.ErrorMessage = "Usuário e senha não correspondem";
                return View();
            }

            string token = _tokenService.GenerateToken(user);

            HttpContext.Session.SetString("UserToken", token);



            return RedirectToAction("Index", "Chat");
        }
    }
}