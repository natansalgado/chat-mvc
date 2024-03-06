using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            string userToken = HttpContext.Session.GetString("UserToken");
            string userId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userToken))
            {
                HttpContext.Session.SetString("UserToken", "");

                return RedirectToAction("Index", "Login");
            }

            ViewBag.userId = userId;

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("UserToken", "");

            return RedirectToAction("");
        }
    }
}