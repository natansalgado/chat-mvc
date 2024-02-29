using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Dtos;
using mvc.Exceptions;
using mvc.Services.Interfaces;

namespace mvc.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserService _userService;

        public RegisterController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
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
                _userService.Create(createUserDto);
                return View("Created");
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

        public ActionResult Created()
        {
            return View();
        }
    }
}