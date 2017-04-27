using System;
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
    public class GruppController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: Grupp
        public ActionResult Index()
        {
            return View(db.Grupps.ToList());
        }

        // GET: Grupp/Details/5
        public ActionResult Details(int? id)
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
        public ActionResult Create([Bind(Include = "GruppID,GruppNamn")] Grupp grupp)
        {
            if (ModelState.IsValid)
            {
                db.Grupps.Add(grupp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grupp);
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
        public ActionResult Edit([Bind(Include = "GruppID,GruppNamn")] Grupp grupp)
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

        //vy till grupper som användare tillhör
        public ActionResult MinaGrupper()
        {
            return View();
        }
    }
}
