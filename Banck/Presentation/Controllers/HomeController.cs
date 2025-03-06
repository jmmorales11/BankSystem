using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                return RedirectToAction("Login", "User");
            }
            var email = TempData["Email"] as string; // Obtener el correo guardado
            ViewBag.Email = email; // Pasar a la vista

            return View();
        }   

        public ActionResult About()
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                return RedirectToAction("Login", "User");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            // Recuperar el token de la sesión (ejemplo)
            var token = Session["JWT_Token"] as string;
            if (string.IsNullOrEmpty(token))
            {
                TempData["ErrorMessage"] = "Por favor, inicia sesión.";
                return RedirectToAction("Login", "User");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}