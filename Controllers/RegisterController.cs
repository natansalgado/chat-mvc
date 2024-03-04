using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Dtos;
using mvc.Exceptions;
using mvc.Models;
using mvc.Services.Interfaces;

namespace mvc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public RegisterController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
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
        public ActionResult Index(CreateUserDto createUserDto)
        {
            if (createUserDto.Password != createUserDto.ConfirmPassword)
            {
                ViewBag.ErrorMessage = "Confirmação de senha inválida, utilize a mesma senha.";
                return View();
            }

            try
            {
                UserModel user = _userService.Create(createUserDto);

                string token = _tokenService.GenerateToken(user);

                HttpContext.Session.SetString("UserToken", token);

                return RedirectToAction("Index", "Chat");
            }
            catch (ChatException ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Erro ao tentar acessar o servidor, tente novamente mais tarde.";
                return View();
            }
        }
    }
}