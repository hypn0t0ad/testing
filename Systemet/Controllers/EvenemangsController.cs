using System;
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
    public class EvenemangsController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: Evenemangs
        public ActionResult Index()
        {
            return View(db.Evenemangs.ToList());
        }

        // GET: Evenemangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenemang evenemang = db.Evenemangs.Find(id);
            if (evenemang == null)
            {
                return HttpNotFound();
            }
            return View(evenemang);
        }

        // GET: Evenemangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evenemangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EvenemangID,Namn,Beskrivning,Tidpunkt, Plats")] Evenemang evenemang, Grupp grupp )
        {          
            int gID = Convert.ToInt32(Session["GruppID"]);



            grupp = db.Grupps.Single(m => m.GruppID == gID);
            evenemang.grupp = grupp;

            db.Evenemangs.Add(evenemang);
            db.SaveChanges();
            TempData["eventID"] = evenemang.EvenemangID;
            return RedirectToAction("evenemangssida");
        }

        // GET: Evenemangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenemang evenemang = db.Evenemangs.Find(id);
            if (evenemang == null)
            {
                return HttpNotFound();
            }
            return View(evenemang);
        }

        // POST: Evenemangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EvenemangID,Namn,Beskrivning,StartTid,SlutTid,Dag")] Evenemang evenemang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evenemang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evenemang);
        }

        // GET: Evenemangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evenemang evenemang = db.Evenemangs.Find(id);
            if (evenemang == null)
            {
                return HttpNotFound();
            }
            return View(evenemang);
        }

        // POST: Evenemangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evenemang evenemang = db.Evenemangs.Find(id);
            db.Evenemangs.Remove(evenemang);
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

        public ActionResult evenemangssida(int? idet)
        {
            int ID = Convert.ToInt32(TempData["eventID"]);
            Evenemang eventet;
            if (idet.HasValue)
            {
                eventet = db.Evenemangs.SingleOrDefault(a => a.EvenemangID == idet);
            }
            else
            {
                eventet = db.Evenemangs.SingleOrDefault(a => a.EvenemangID == ID);
            }
            var kommentarer = db.EvenemangsKommentarers.Where(r => r.evenemang.EvenemangID == eventet.EvenemangID);
            ICollection<EvenemangsKommentarer> comments;
            comments = kommentarer.ToList();
            return View(Tuple.Create(eventet, comments));
        }
    }
}
