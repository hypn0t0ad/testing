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
    public class GruppFörfråganController : Controller
    {
        private SystemetDBContext db = new SystemetDBContext();

        // GET: GruppFörfrågan
        public ActionResult Index()
        {
            return View(db.GruppFörfrågan.ToList());
        }

        // GET: GruppFörfrågan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppFörfrågan gruppFörfrågan = db.GruppFörfrågan.Find(id);
            if (gruppFörfrågan == null)
            {
                return HttpNotFound();
            }
            return View(gruppFörfrågan);
        }

        // GET: GruppFörfrågan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GruppFörfrågan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FörfråganID,Godkänd,text")] GruppFörfrågan gruppFörfrågan)
        {
            if (ModelState.IsValid)
            {
                db.GruppFörfrågan.Add(gruppFörfrågan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gruppFörfrågan);
        }

        // GET: GruppFörfrågan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppFörfrågan gruppFörfrågan = db.GruppFörfrågan.Find(id);
            if (gruppFörfrågan == null)
            {
                return HttpNotFound();
            }
            return View(gruppFörfrågan);
        }

        // POST: GruppFörfrågan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FörfråganID,Godkänd,text")] GruppFörfrågan gruppFörfrågan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gruppFörfrågan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gruppFörfrågan);
        }

        // GET: GruppFörfrågan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GruppFörfrågan gruppFörfrågan = db.GruppFörfrågan.Find(id);
            if (gruppFörfrågan == null)
            {
                return HttpNotFound();
            }
            return View(gruppFörfrågan);
        }

        // POST: GruppFörfrågan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GruppFörfrågan gruppFörfrågan = db.GruppFörfrågan.Find(id);
            db.GruppFörfrågan.Remove(gruppFörfrågan);
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

        public ActionResult SkickaAnsökan(string id)
        {
            int anvid = Convert.ToInt32(TempData["AnvändarID"]);

            SystemetDBContext db = new SystemetDBContext();

            AnvändarKonton konto = db.konton.SingleOrDefault(k => k.AnvändarID == anvid);
            Grupp grupp = db.Grupps.SingleOrDefault(g => g.GruppNamn == id);

            

            if (!grupp.GruppMedlemmar.Contains(konto))
            {
                GruppFörfrågan ansökan = new GruppFörfrågan();
                ansökan.AnvändareSomFrågar = konto;
                ansökan.GruppFörfråganGäller = grupp;
                ansökan.text = "behandlas";
                konto.Föfrågningar.Add(ansökan);
                grupp.Ansökningar.Add(ansökan);
                db.SaveChanges();


                konto.AnvändarID = Convert.ToInt32(TempData["användarID"]);
            }

            
            //TempData["aid"] = ansökan.FörfråganID;
            //TempData["gruppNamn"] = ansökan.GruppFörfråganGäller.GruppNamn;
            //TempData["gruppID"] = ansökan.GruppFörfråganGäller.GruppID;

            return RedirectToAction("inloggad", "Konto");
        }
    }
}
