using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvc.Controllers
{
    public class ChatController : Controller
    {
        public ActionResult Index()
        {
            string userToken = HttpContext.Session.GetString("UserToken");

            if (string.IsNullOrEmpty(userToken))
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.SetString("UserToken", "");

            return RedirectToAction("");
        }
    }
}