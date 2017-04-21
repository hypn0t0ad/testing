using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;

namespace Systemet.Controllers
{
    public class KontoController : Controller
    {
        // GET: Konto
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.konton.ToList());
            }
        }

        public  ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AnvändarKonton Konto)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.konton.Add(Konto);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = Konto.FörNamn + " " + Konto.EfterNamn + " färdig med registreringen!";
            }
            return View();
        }

        //login 
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AnvändarKonton user)
        {
            using (OurDBContext db = new OurDBContext())
            {
                var usr = db.konton.Single(u => u.Email == user.Email && u.Password == user.Password);
                if (usr != null)
                {
                    Session["AnvändarID"] = usr.AnvändarID.ToString();
                    Session["Förnamn"] = usr.FörNamn.ToString();
                    Session["Email"] = usr.Email.ToString();
                    return RedirectToAction("inloggad");
                }
                else
                {
                    ModelState.AddModelError("", "email eller lösenordet är felaktigt");
                }
            }
            return View();
        }

        public ActionResult inloggad()
        {
            if (Session["AnvändarID"] != null)
            {
                return View();
            }
            else
            {
                //return RedirectToAction("inloggad");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Redigera()
        {
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            using (OurDBContext db = new OurDBContext())
            {
                if (Session["AnvändarID"] != null)
                {
                    var usr = db.konton.Single(u => u.AnvändarID == ID);
                    return View(usr);
                }
                
            }
            return View();
        }

        [HttpPost]
        public ActionResult Redigera([Bind(Include = "AnvändarID,Förnamn,Efternamn,Epost,Telefon,Password")] AnvändarKonton konto )
        {

            return View("index");
        }

    }
}