using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Systemet.Models;
using Systemet.Models.ViewModels;

namespace vagina.Controllers
{
    public class UppgifterController : Controller
    {
        private SystemetDBContext db = new SystemetDBContext();

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
        public ActionResult Create(int? id)
        {
            Grupp gruppen = db.Grupps.SingleOrDefault(g => g.GruppID == id);
            ViewBag.gruppnamnet = gruppen.GruppNamn;
            return View();
        }

        // POST: Uppgifter/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UppgifterID,Namn,Beskrivning,Slutdatum")] Uppgifter uppgifter, Grupp grupp, AnvändarKonton konto)
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
        public ActionResult Edit(int? id, string gruppnamn)
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
            TempData["nygrupp"] = gruppnamn;
            return View(uppgifter);
        }

        // POST: Uppgifter/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UppgifterID,Namn,Beskrivning,Utförd,Påbörjad,Slutdatum")] Uppgifter uppgifter, string gruppnamn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uppgifter).State = EntityState.Modified;
                
                db.SaveChanges();
                TempData["nygrupp"] = gruppnamn;
                return RedirectToAction("gruppsida", "Grupp");
            }
            return View(uppgifter);


        }

        public ActionResult DeleteConfirmed(int id)
        {
            Uppgifter uppgifter = db.Uppgifters.Find(id);
            Grupp grp = db.Grupps.SingleOrDefault(g => g.GruppID == uppgifter.TillhörGrupp.GruppID);
            db.Uppgifters.Remove(uppgifter);
            db.SaveChanges();
            TempData["nygrupp"] = grp.GruppNamn;
            return RedirectToAction("gruppsida", "Grupp");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult UppgiftsSida(int? idet)
        {
            int inloggadID = Convert.ToInt32(Session["AnvändarID"]);
            int tempID = Convert.ToInt32(TempData["ID"]);
            Uppgifter uppgiften;
            if (idet.HasValue)
            {
                uppgiften = db.Uppgifters.SingleOrDefault(a => a.UppgifterID == idet);
            }
            else
            {
                uppgiften = db.Uppgifters.SingleOrDefault(a => a.UppgifterID == tempID);
            }
            int tillhörgrupp = Convert.ToInt32(uppgiften.TillhörGrupp.GruppID);
            Grupp gruppen = db.Grupps.SingleOrDefault(g => g.GruppID == tillhörgrupp);
            AnvändarKonton inloggad = db.konton.SingleOrDefault(i => i.AnvändarID == inloggadID);
            ICollection<AnvändarKonton> medlemmar = gruppen.GruppMedlemmar;
            
            var gruppviewmodel = new GruppViewModel();
            gruppviewmodel.grupp = gruppen;
            gruppviewmodel.uppgift = uppgiften;
            gruppviewmodel.medlemmar = medlemmar;            
            gruppviewmodel.ledareID = gruppen.LedareID;
            gruppviewmodel.inloggad = inloggad.AnvändarID;
            List<SelectListItem> namnen = new List<SelectListItem>();

            
            foreach (var item in medlemmar)
            {

               namnen.Add(new SelectListItem{ Value = item.AnvändarID.ToString(), Text = item.HelaNamnet });
                
            }
            gruppviewmodel.lemmar = new SelectList(namnen, "Value", "Text");
            return View(gruppviewmodel);
        }

        [HttpPost]
        public ActionResult läggtillanvändaretilluppgift(GruppViewModel gvp)
        {
            int ansvarig = gvp.användareID;            
            int uppgiftID = gvp.uppgift.UppgifterID;

            Grupp grupp = db.Grupps.SingleOrDefault(g => g.GruppID == gvp.grupp.GruppID);
            Uppgifter uppgift = db.Uppgifters.SingleOrDefault(u => u.UppgifterID == uppgiftID);
            AnvändarKonton ansvariganvändare = db.konton.SingleOrDefault(a => a.AnvändarID == ansvarig);
            uppgift.Ansvarig = ansvariganvändare;
            ansvariganvändare.AnsvararFörUppgift.Add(uppgift);            
            db.SaveChanges();
            TempData["ID"] = uppgift.UppgifterID;
            TempData["nygrupp"] = grupp.GruppNamn;
            return RedirectToAction("gruppsida", "Grupp");
        }

        [HttpPost]
        public ActionResult tapåsiguppgift(GruppViewModel gvp)
        {
            int ansvarig = gvp.inloggad;
            int uppgiftID = gvp.uppgift.UppgifterID;

            Grupp grupp = db.Grupps.SingleOrDefault(g => g.GruppID == gvp.grupp.GruppID);
            Uppgifter uppgift = db.Uppgifters.SingleOrDefault(u => u.UppgifterID == uppgiftID);
            AnvändarKonton ansvariganvändare = db.konton.SingleOrDefault(a => a.AnvändarID == ansvarig);
            uppgift.Ansvarig = ansvariganvändare;
            ansvariganvändare.AnsvararFörUppgift.Add(uppgift);
            db.SaveChanges();
            TempData["ID"] = uppgift.UppgifterID;
            TempData["nygrupp"] = grupp.GruppNamn;
            return RedirectToAction("gruppsida", "Grupp");
        }

        [HttpPost]
        public ActionResult påbörja()
        {

            return RedirectToAction("UppgiftsSida");
        }

        [HttpPost]
        public ActionResult avsluta()
        {

            return RedirectToAction("UppgiftsSida");
        }

        public ActionResult tabortfrånlista(int? id, int? grupp)
        {
            Grupp grp = db.Grupps.SingleOrDefault(g => g.GruppID == grupp);
            Uppgifter upg = db.Uppgifters.SingleOrDefault(u => u.UppgifterID == id);
            upg.bortplockad = true;
            upg.Utförd = false;
            db.SaveChanges();
            TempData["nygrupp"] = grp.GruppNamn;
            return RedirectToAction("gruppsida", "Grupp");
        }
    }
}
