using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ohjelmistoprojekti.Models;

namespace Ohjelmistoprojekti.Controllers
{
    public class HenkilonOsaamisController : Controller
    {
        private ohjelmistodataEntities db = new ohjelmistodataEntities();

        // GET: HenkilonOsaamis
        //public ActionResult Index()
        //{
        //    var henkilonOsaamiset = db.HenkilonOsaamiset.Include(h => h.Henkilot).Include(h => h.Osaamisaiheet);
        //    return View(henkilonOsaamiset.ToList());
        //}

        public ActionResult Index()
        {
            List<SimplyOsaamisrekisteriData> model = new List<SimplyOsaamisrekisteriData>();

            ohjelmistodataEntities entities = new ohjelmistodataEntities();

            try
            {
                List<Henkilot> henkilot = entities.Henkilot.OrderBy(Henkilot => Henkilot.Sukunimi).ToList();

                // muodostetaan näkymämalli tietokannan rivien pohjalta       
                foreach (Henkilot hlo in henkilot)
                {
                    SimplyOsaamisrekisteriData view = new SimplyOsaamisrekisteriData();    
                    view.HenkiloID = hlo.HenkiloID;
                    view.Etunimi = hlo.Etunimi;
                    view.Sukunimi = hlo.Sukunimi;
                    view.TyoPuhelin = hlo.TyoPuhelin;
                    view.TyoSahkoposti = hlo.TyoSahkoposti;
                    view.Organiaatio = hlo.Organiaatio;
                    view.Henkilonumero = hlo.Henkilonumero;
                    
                    view.HenkilonOsaamisID = hlo.HenkilonOsaamiset?.FirstOrDefault()?.HenkilonOsaamisID;
                    view.Osaamistaso = hlo.HenkilonOsaamiset?.FirstOrDefault()?.Osaamistaso;

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }
            return View(model);
        }//Index

        CultureInfo fiFi = new CultureInfo("fi-FI");
    

        // GET: HenkilonOsaamis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HenkilonOsaamiset henkilonOsaamiset = db.HenkilonOsaamiset.Find(id);
            if (henkilonOsaamiset == null)
            {
                return HttpNotFound();
            }
            return View(henkilonOsaamiset);
        }

        // GET: HenkilonOsaamis/Create
        public ActionResult Create()
        {
            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi");
            ViewBag.OsaamisaiheID = new SelectList(db.Osaamisaiheet, "OsaamisaiheID", "Kuvaus");
            return View();
        }

        // POST: HenkilonOsaamis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HenkilonOsaamisID,OsaamisaiheID,HenkiloID,Osaamistaso")] HenkilonOsaamiset henkilonOsaamiset)
        {
            if (ModelState.IsValid)
            {
                db.HenkilonOsaamiset.Add(henkilonOsaamiset);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi", henkilonOsaamiset.HenkiloID);
            ViewBag.OsaamisaiheID = new SelectList(db.Osaamisaiheet, "OsaamisaiheID", "Kuvaus", henkilonOsaamiset.OsaamisaiheID);
            return View(henkilonOsaamiset);
        }

        // GET: HenkilonOsaamis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HenkilonOsaamiset henkilonOsaamiset = db.HenkilonOsaamiset.Find(id);
            if (henkilonOsaamiset == null)
            {
                return HttpNotFound();
            }
            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi", henkilonOsaamiset.HenkiloID);
            ViewBag.OsaamisaiheID = new SelectList(db.Osaamisaiheet, "OsaamisaiheID", "Kuvaus", henkilonOsaamiset.OsaamisaiheID);
            return View(henkilonOsaamiset);
        }

        // POST: HenkilonOsaamis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HenkilonOsaamisID,OsaamisaiheID,HenkiloID,Osaamistaso")] HenkilonOsaamiset henkilonOsaamiset)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilonOsaamiset).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi", henkilonOsaamiset.HenkiloID);
            ViewBag.OsaamisaiheID = new SelectList(db.Osaamisaiheet, "OsaamisaiheID", "Kuvaus", henkilonOsaamiset.OsaamisaiheID);
            return View(henkilonOsaamiset);
        }

        // GET: HenkilonOsaamis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HenkilonOsaamiset henkilonOsaamiset = db.HenkilonOsaamiset.Find(id);
            if (henkilonOsaamiset == null)
            {
                return HttpNotFound();
            }
            return View(henkilonOsaamiset);
        }

        // POST: HenkilonOsaamis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HenkilonOsaamiset henkilonOsaamiset = db.HenkilonOsaamiset.Find(id);
            db.HenkilonOsaamiset.Remove(henkilonOsaamiset);
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


        public ActionResult GetHenkiloOsaamiset(int? id)
        {
            ohjelmistodataEntities entities = new ohjelmistodataEntities();

            List<HenkilonOsaamiset> hlosaamiset = (from ho in entities.HenkilonOsaamiset
                                                   where ho.HenkilonOsaamisID == id
                                                   select ho).ToList();

            List<SimplyOsaamisrekisteriData> result = new List<SimplyOsaamisrekisteriData>();

            foreach (HenkilonOsaamiset hlosa in hlosaamiset)
            {
                SimplyOsaamisrekisteriData data = new SimplyOsaamisrekisteriData();

                data.HenkilonOsaamisID = hlosa.HenkilonOsaamisID;
                data.HenkiloID = (int)hlosa.HenkiloID;
                data.OsaamisaiheID = (int)hlosa.OsaamisaiheID;

                data.Osaamistaso = (int)hlosa.Osaamistaso;

                List<Henkilot> henkilot = (from h in entities.Henkilot
                                           where h.HenkiloID == hlosa.HenkiloID
                                           select h).ToList();

                data.Etunimi = henkilot[0].Etunimi;
                data.Sukunimi = henkilot[0].Sukunimi;
                data.Organiaatio = henkilot[0].Organiaatio;

                List<Osaamisaiheet> osaamisaihe = (from o in entities.Osaamisaiheet
                                                   where o.OsaamisaiheID == hlosa.OsaamisaiheID
                                                   select o).ToList();

                data.Kuvaus = osaamisaihe[0].Kuvaus;

                result.Add(data);
            }

            entities.Dispose();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
