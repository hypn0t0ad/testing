﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;

namespace vagina.Controllers
{
    public class EvenemangsKommentarersController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: EvenemangsKommentarers
        public ActionResult Index()
        {
            return View(db.EvenemangsKommentarers.ToList());
        }

        // GET: EvenemangsKommentarers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenemangsKommentarer evenemangsKommentarer = db.EvenemangsKommentarers.Find(id);
            if (evenemangsKommentarer == null)
            {
                return HttpNotFound();
            }
            return View(evenemangsKommentarer);
        }

        // GET: EvenemangsKommentarers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvenemangsKommentarers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvenemangsKommentarerID,Text")] EvenemangsKommentarer evenemangsKommentarer)
        {
            evenemangsKommentarer.TidenFörKommentaren = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.EvenemangsKommentarers.Add(evenemangsKommentarer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("evenemangssida", "evenemangs");
        }

        // GET: EvenemangsKommentarers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenemangsKommentarer evenemangsKommentarer = db.EvenemangsKommentarers.Find(id);
            if (evenemangsKommentarer == null)
            {
                return HttpNotFound();
            }
            return View(evenemangsKommentarer);
        }

        // POST: EvenemangsKommentarers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvenemangsKommentarerID,TidenFörKommentaren,Text")] EvenemangsKommentarer evenemangsKommentarer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenemangsKommentarer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evenemangsKommentarer);
        }

        // GET: EvenemangsKommentarers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvenemangsKommentarer evenemangsKommentarer = db.EvenemangsKommentarers.Find(id);
            if (evenemangsKommentarer == null)
            {
                return HttpNotFound();
            }
            return View(evenemangsKommentarer);
        }

        // POST: EvenemangsKommentarers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvenemangsKommentarer evenemangsKommentarer = db.EvenemangsKommentarers.Find(id);
            db.EvenemangsKommentarers.Remove(evenemangsKommentarer);
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

      
        public ActionResult komments(int? id)
        {
            Evenemang eventet = db.Evenemangs.SingleOrDefault(e => e.EvenemangID == id);
            ViewBag.rättevent = eventet;
            return PartialView("komments");
        }

        [HttpPost]
        public ActionResult komments([Bind(Include = "EvenemangsKommentarerID,Text")] EvenemangsKommentarer evenemangsKommentarer, int? id)
        {
            Evenemang evnt = db.Evenemangs.SingleOrDefault(u => u.EvenemangID == id);
            evenemangsKommentarer.evenemang = evnt;
            evenemangsKommentarer.TidenFörKommentaren = DateTime.Now;
            
            if (ModelState.IsValid)
            {
                db.EvenemangsKommentarers.Add(evenemangsKommentarer);
                evnt.Åsikter.Add(evenemangsKommentarer);
                ICollection<EvenemangsKommentarer> synpunkter;
                synpunkter = evnt.Åsikter;
                db.SaveChanges();
                return RedirectToAction("evenemangssida", "evenemangs", Tuple.Create( evnt, evenemangsKommentarer, synpunkter));
            }

            return View("evenemangssida", "evenemangs");
        }
    }
}