﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.Security;
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
            return View();
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

            List<Grupp> grupperna = new List<Grupp>();
            var allagrupper = db.Grupps.Select(g => g.GruppNamn).ToList();
            List<Evenemang> evenemang = new List<Evenemang>();
            List<Uppgifter> uppgifts = new List<Uppgifter>();

            grupperna = user.TillhörGrupper.ToList();   //PROBLEM NULL-REFERENCE

            foreach (var item in grupperna)
            {
                evenemang.AddRange(db.Evenemangs.Where(e => e.grupp.GruppID == item.GruppID).ToList());
                uppgifts.AddRange(db.Uppgifters.Where(u => u.TillhörGrupp.GruppID == item.GruppID).ToList());
            }


            if (Session["AnvändarID"] != null)
            {
                ViewBag.allagrupper = allagrupper;
                return View(Tuple.Create(user, grupperna, evenemang, uppgifts));
            }
            else
            {
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

        public ActionResult LogOut()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}