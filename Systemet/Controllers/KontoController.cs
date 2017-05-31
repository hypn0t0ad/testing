using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
using Systemet.Models;
using Systemet.Models.ViewModels;

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
                TempData["välkommen"] = "Välkommen " + Konto.FörNamn + " " + Konto.EfterNamn +"! " + "Du är färdig med registreringen! Men du måste ändå logga in för att bekräfta.";
            }
            return RedirectToAction("Index", "Home");
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
                AnvändarKonton konto = db.konton.SingleOrDefault(u => u.Email == user.Email && u.Password == user.Password);
                user = konto;
                if (konto != null)
                {
                    Session["AnvändarID"] = konto.AnvändarID.ToString();
                    Session["Förnamn"] = konto.FörNamn.ToString();
                    Session["Email"] = konto.Email.ToString();
                    return RedirectToAction("inloggad", user);
                }
                else
                {
                    ModelState.AddModelError("", "email eller lösenordet är felaktigt");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult inloggad(AnvändarKonton user)
        {
            int ID;
            OurDBContext db = new OurDBContext();
            AnvändarKonton konto;

            if (user.Email == null)
            {
                ID = Convert.ToInt32(TempData["användarID"]);
                konto = db.konton.SingleOrDefault(u => u.AnvändarID == ID);
            }
            else
            {
                konto = db.konton.SingleOrDefault(u => u.AnvändarID == user.AnvändarID);
            }
            user = konto;

            var inloggadviewmodel = new InloggadViewModel();
            inloggadviewmodel.användare = user;
            inloggadviewmodel.Grupperna = user.TillhörGrupper.ToList();
            List<Evenemang> evenemang = new List<Evenemang>();
            List<Uppgifter> uppgifts = new List<Uppgifter>();
            foreach (var item in inloggadviewmodel.Grupperna)
            {
                evenemang.AddRange(db.Evenemangs.Where(e => e.grupp.GruppID == item.GruppID).ToList());
                uppgifts.AddRange(db.Uppgifters.Where(u => u.TillhörGrupp.GruppID == item.GruppID).ToList());
            }
            inloggadviewmodel.Evenemangen = evenemang;
            inloggadviewmodel.uppgifterna = uppgifts;
            inloggadviewmodel.ansökningarna = user.Föfrågningar.ToList();
            
            return View(inloggadviewmodel);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
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
        [ValidateAntiForgeryToken]
        public ActionResult Redigera([Bind(Include = "AnvändarID,Förnamn,Efternamn,Email,Telefon,Password,ConfirmPassword")] AnvändarKonton konto)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.Entry(konto).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                TempData["användarID"] = konto.AnvändarID;
                return RedirectToAction("inloggad", "Konto");
            }

            return RedirectToAction("Redigera", "Konto");
        }
    }
}