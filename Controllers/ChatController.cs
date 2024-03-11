using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using mvc.Dtos;
using mvc.Exceptions;
using mvc.Models;
using mvc.Services.Interfaces;

namespace mvc.Controllers
{
    public class ChatController : Controller
    {
        private readonly IUserService _service;

        public ChatController(IUserService service)
        {
            _service = service;
        }

        public ActionResult Index()
        {
            string userToken = HttpContext.Session.GetString("UserToken");

            if (string.IsNullOrEmpty(userToken))
            {
                HttpContext.Session.SetString("UserToken", "");
                HttpContext.Session.SetObject("UserModel", null);

                return RedirectToAction("Index", "Login");
            }

            UserModel userModel = HttpContext.Session.GetObject<UserModel>("UserModel");

            return View(userModel);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("UserToken", "");

            return RedirectToAction("");
        }

        public ActionResult Edit()
        {
            UserModel userModel = HttpContext.Session.GetObject<UserModel>("UserModel");

            return View(userModel);
        }

        [HttpPost]
        public ActionResult Edit(UserModel userModel)
        {
            UpdateUserDto updateUserDto = new();

            if (userModel.Avatar != null) updateUserDto.Avatar = userModel.Avatar;
            if (userModel.UserName != null) updateUserDto.UserName = userModel.UserName;

            UserModel user = HttpContext.Session.GetObject<UserModel>("UserModel");

            try
            {
                UserModel userUpdated = _service.Update(user.Id, updateUserDto);

                HttpContext.Session.SetObject("UserModel", userUpdated);

                return RedirectToAction("Index", "Chat");
            }
            catch (ChatException ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View(user);
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Erro ao tentar se conectar com o servidor.";

                return View(user);
            }
        }
    }
}