﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;

namespace Systemet.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
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
        public ActionResult Register(AnvändarKonton account)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.konton.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FörNamn + " " + account.EfterNamn + " färdig med registreringen!";
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
                var usr = db.konton.Single(u => u.FörNamn == user.FörNamn && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserID"] = usr.AnvändarID.ToString();
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
                return RedirectToAction("inloggad");
            }
        }
    }
}