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
    public class UppgifterController : Controller
    {
        private OurDBContext db = new OurDBContext();

        // GET: Uppgifter
        public ActionResult Index()
        {
            return View(db.Uppgifters.ToList());
        }

        // GET: Uppgifter/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uppgifter uppgifter = db.Uppgifters.Find(id);
            if (uppgifter == null)
            {
                return HttpNotFound();
            }
            return View(uppgifter);
        }

        // GET: Uppgifter/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Uppgifter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UppgifterID,Namn,Beskrivning,Startdatum")] Uppgifter uppgifter, Grupp grupp, AnvändarKonton konto)
        {

            int gID = Convert.ToInt32(Session["GruppID"]);

            grupp = db.Grupps.Single(g => g.GruppID == gID);

            uppgifter.TillhörGrupp = grupp;


            db.Uppgifters.Add(uppgifter);
            db.SaveChanges();
            TempData["nygrupp"] = grupp.GruppNamn;

            return RedirectToAction("gruppsida", "Grupp");
        }

        // GET: Uppgifter/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uppgifter uppgifter = db.Uppgifters.Find(id);
            if (uppgifter == null)
            {
                return HttpNotFound();
            }
            return View(uppgifter);
        }

        // POST: Uppgifter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UppgifterID,Namn,Beskrivning,Utförd,Påbörjad,Startdatum")] Uppgifter uppgifter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uppgifter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uppgifter);
        }

        // GET: Uppgifter/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Uppgifter uppgifter = db.Uppgifters.Find(id);
            if (uppgifter == null)
            {
                return HttpNotFound();
            }
            return View(uppgifter);
        }

        // POST: Uppgifter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Uppgifter uppgifter = db.Uppgifters.Find(id);
            db.Uppgifters.Remove(uppgifter);
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


        public ActionResult UppgiftsSida (int? idet)
        {

            Uppgifter uppgiften;
            if (idet.HasValue)
            {
                uppgiften = db.Uppgifters.SingleOrDefault(a => a.UppgifterID == idet);
            }
            else
            {
                return View("Index", "Home");
            }

            int tillhörgrupp = Convert.ToInt32(uppgiften.TillhörGrupp.GruppID);
            Grupp gruppen = db.Grupps.SingleOrDefault(g => g.GruppID == tillhörgrupp);
            ICollection<AnvändarKonton> medlemmar = gruppen.GruppMedlemmar;

            var namnen = new List<SelectListItem>();
            namnen.Add(new SelectListItem { Value = "0", Text = "Välj ansvarig" });

            int value = 1;
            foreach (var item in medlemmar)
            {
                
                namnen.Add(new SelectListItem { Value = value.ToString(), Text = item.FörNamn + " " + item.EfterNamn });
                value++;
            }
            ViewData["AnsvarigaNamn"] = namnen;

            return View(Tuple.Create(uppgiften, gruppen));
        }

        [HttpPost]
        public ActionResult läggtillanvändaretilluppgift(int? ID)
        {
            return RedirectToAction("UppgiftsSida");
        }
    }
}
