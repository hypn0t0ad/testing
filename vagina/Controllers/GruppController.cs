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
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            grupp.LedareID = user.AnvändarID;
            db.Grupps.Add(grupp);
            grupp.GruppMedlemmar.Add(user);
            user.TillhörGrupper.Add(grupp);
            db.SaveChanges();

            return View("index");            
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
        public ActionResult Edit([Bind(Include = "GruppID,GruppNamn,LedareID")] Grupp grupp)
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
            return View();
        }

        [HttpPost]
        public ActionResult gåmedigrupp(string sökning)
        {   
            
            Grupp grupp = db.Grupps.Where(f => f.GruppNamn == sökning).FirstOrDefault();

            if (grupp != null)
            {
                ViewBag.gruppen = grupp.GruppNamn;
                TempData["gruppnamn"] = grupp.GruppNamn;
                return View("bekräftagrupp");
            }
            return View("error", "konto");
        }

        public ActionResult minagrupper()
        {
            int ID = Convert.ToInt32(Session["AnvändarID"]);
            AnvändarKonton user = db.konton.Single(u => u.AnvändarID == ID);
            
            ViewBag.minagrupper = user.TillhörGrupper;

            return View();
        }

        
        
    }
}
