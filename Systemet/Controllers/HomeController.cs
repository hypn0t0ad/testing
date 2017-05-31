using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;

namespace Systemet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (TempData["välkommen"] != null)
            {
                ViewBag.Message = TempData["välkommen"].ToString();
            }
           
            return View();
        }

        [HttpPost]
        public ActionResult Index(AnvändarKonton user)
        {
            using (OurDBContext db = new OurDBContext())
            {
                AnvändarKonton konto = db.konton.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                user = konto;
                if (konto != null)
                {
                    Session["AnvändarID"] = konto.AnvändarID.ToString();
                    Session["Förnamn"] = konto.FörNamn.ToString();
                    Session["Email"] = konto.Email.ToString();
                    return RedirectToAction("inloggad", "Konto", user);
                }
                else
                {
                    ModelState.AddModelError("", "email eller lösenordet är felaktigt");
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}