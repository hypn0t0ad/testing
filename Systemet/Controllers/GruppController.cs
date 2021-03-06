﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;

namespace Systemet.Controllers
{
    public class GruppController : Controller
    {
        private SystemetDBContext db = new SystemetDBContext();

        // GET: Grupp
        public ActionResult Index()
        {
            return View();
        }

        // GET: Grupp/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupp grupp = db.Grupps.Find(id);
            int ID = grupp.LedareID;
            AnvändarKonton ledare = db.konton.Single(a => a.AnvändarID == ID);
            ViewBag.ledaren = ledare.FörNamn + " " + ledare.EfterNamn;
            if (grupp == null)
            {
                return HttpNotFound();
            }
            return View(Tuple.Create(grupp, ledare));
        }

        // GET: Grupp/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grupp/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GruppID,GruppNamn, beskrivning")] Grupp grupp)
        {
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            grupp.LedareID = user.AnvändarID;
            db.Grupps.Add(grupp);
            grupp.GruppMedlemmar.Add(user);
            user.TillhörGrupper.Add(grupp);
            db.SaveChanges();
            TempData["nygrupp"] = grupp.GruppNamn;

            return RedirectToAction("gruppsida");            
        }

        [HttpPost]
        public ActionResult blimedlemigrupp(string namn)
        {
            Grupp grupp = db.Grupps.Single(a => a.GruppNamn == namn);
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            grupp.GruppMedlemmar.Add(user);
            user.TillhörGrupper.Add(grupp);
            db.SaveChanges();

            return View("index");
        }

        //den här används inte än
        public ActionResult läggtilligruppen(Grupp grupp)
        {
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            
            grupp.GruppMedlemmar.Add(user);
            db.SaveChanges();
            return View();
        }

        public ActionResult taborturgruppen(int ? id, int ? gid)
        {
            SystemetDBContext db = new SystemetDBContext();

            AnvändarKonton user = db.konton.Find(id);
            Grupp grupp = db.Grupps.Find(gid);
            if (user.AnvändarID == grupp.LedareID)
            {
                grupp.GruppMedlemmar.Remove(user);
                user.TillhörGrupper.Remove(grupp);
                var medlemmar = grupp.GruppMedlemmar.ToList();
                grupp.LedareID = medlemmar.FirstOrDefault().AnvändarID;
                db.SaveChanges();
                return RedirectToAction("gruppsida", "Grupp", grupp);
            }
            else
            {
                grupp.GruppMedlemmar.Remove(user);
                user.TillhörGrupper.Remove(grupp);
                db.SaveChanges();
                return RedirectToAction("gruppsida", "Grupp", grupp);
            }            
        }

        // GET: Grupp/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupp grupp = db.Grupps.Find(id);
            if (grupp == null)
            {
                return HttpNotFound();
            }
            return View(grupp);
        }

        // POST: Grupp/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GruppID,GruppNamn,LedareID, beskrivning")] Grupp grupp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grupp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grupp);
        }

        // GET: Grupp/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupp grupp = db.Grupps.Find(id);
            if (grupp == null)
            {
                return HttpNotFound();
            }
            return View(grupp);
        }

        // POST: Grupp/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupp grupp = db.Grupps.Find(id);
            db.Grupps.Remove(grupp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult gåmedigrupp()
        {
            var grupperna = TempData["allagrupper"];

            return View();
        }

        [HttpPost]
        public ActionResult gåmedigrupp(string sökning)
        {
            
            SystemetDBContext db = new SystemetDBContext();

            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);

            ICollection<Grupp> allagrupper;

            allagrupper = db.Grupps.Where(g => g.GruppNamn.Contains(sökning)).ToList();
            

            if (allagrupper != null)
            {
                return View(allagrupper);
            }
            ViewBag.noll = "Inga resultat tyvärr!";
            return View();
        }

        public ActionResult minagrupper()
        {
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            
            ViewBag.minagrupper = user.TillhörGrupper;

            return View();
        }

        public ActionResult gruppsida (Grupp grupp)
        {
          
            Grupp gruppen = new Grupp();            
            if (grupp.GruppNamn != null)
            {
                gruppen = db.Grupps.Single(m => m.GruppNamn == grupp.GruppNamn);
            }
            else if (TempData["nygrupp"] != null)
            {
                string namnet = TempData["nygrupp"].ToString();
                gruppen = db.Grupps.Single(m => m.GruppNamn == namnet);
            }
            
            ICollection<Evenemang> events;
            events = gruppen.Evenemang;
            ICollection<Uppgifter> uppgifterna;           
            uppgifterna = gruppen.GruppUppgifter.OrderBy(x => x.Slutdatum).ToList();
            Session["GruppID"] = gruppen.GruppID.ToString();

            return View(Tuple.Create(gruppen, events, uppgifterna));
        }

        public ActionResult HanteraAnsökningar(string idet, int vem, int grupp)
        {
            List<GruppFörfrågan> glist = new List<GruppFörfrågan>();
            Grupp gruppen = db.Grupps.SingleOrDefault(j => j.GruppID == grupp);
            AnvändarKonton anv = db.konton.SingleOrDefault(a => a.AnvändarID == vem);

            glist = db.GruppFörfrågan.Where(g => g.AnvändareSomFrågar.AnvändarID == anv.AnvändarID).ToList();
            GruppFörfrågan ansökning = glist.SingleOrDefault(g => g.GruppFörfråganGäller.GruppID == grupp && g.text != "hide");
            if (idet == "neka" && idet != "godkänn")
            {
                ansökning.Godkänd = false;
                ansökning.text = "nekad";
                
            }
            else if (idet != "neka" && idet == "godkänn")
            {
                ansökning.Godkänd = true;
                ansökning.text = "godkänd";
                anv.TillhörGrupper.Add(gruppen);
                gruppen.GruppMedlemmar.Add(anv);
                db.SaveChanges();
            }
            TempData["nygrupp"] = gruppen.GruppNamn;

            return RedirectToAction("gruppsida", "Grupp");
        }




    }
}
