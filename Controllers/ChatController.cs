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
            string name = HttpContext.Session.GetString("UserName");

            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.UserName = name;
            ViewBag.NameColor = HttpContext.Session.GetString("NameColor");
            return View();
        }
    }
}