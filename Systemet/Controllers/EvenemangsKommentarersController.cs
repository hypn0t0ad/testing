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
            AnvändarKonton användare;
            int anv = Convert.ToInt32(Session["AnvändarID"]);
            användare = db.konton.SingleOrDefault(a => a.AnvändarID == anv);
            evenemangsKommentarer.TidenFörKommentaren = DateTime.Now;
            evenemangsKommentarer.kommentator = användare;
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
            AnvändarKonton användare;

            int anv = Convert.ToInt32(Session["AnvändarID"]);
            användare = db.konton.SingleOrDefault(a => a.AnvändarID == anv);

            evenemangsKommentarer.kommentator = användare;
            evenemangsKommentarer.TidenFörKommentaren = DateTime.Now;

            Evenemang eventet = db.Evenemangs.SingleOrDefault(a => a.EvenemangID == id);

            eventet.Åsikter.Add(evenemangsKommentarer);
            användare.Kommentarer.Add(evenemangsKommentarer);


            db.EvenemangsKommentarers.Add(evenemangsKommentarer);
            db.SaveChanges();
            TempData["eventID"] = eventet.EvenemangID;           

            return RedirectToAction("evenemangssida", "evenemangs");

        }

        public ActionResult Partial()
        {
            var list = new List<Guid>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(Guid.NewGuid());
            }
            ViewBag.List = list;
            return PartialView("_MyPartialView");
        }
    }
}
