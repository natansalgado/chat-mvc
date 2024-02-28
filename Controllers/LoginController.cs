using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        private static readonly List<string> _loggedUsers = new();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string name, string nameColor)
        {
            Console.WriteLine(name);

            if (_loggedUsers.Any(x => x == name))
            {
                ViewBag.ErrorMessage = "Esse nome já está em uso, tente outro.";
                return View();
            }

            if (!string.IsNullOrEmpty(name))
            {
                HttpContext.Session.SetString("UserName", name);
                HttpContext.Session.SetString("NameColor", nameColor);

                _loggedUsers.Add(name);

                return RedirectToAction("Index", "Chat");
            }

            ViewBag.ErrorMessage = "Insira um nome válido.";
            return View();
        }
    }
}