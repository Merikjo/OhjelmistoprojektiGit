using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Ohjelmistoprojekti.Models;

namespace Ohjelmistoprojekti.Controllers
{
    public class OsaamisaiheController : Controller
    {
        private ohjelmistodataEntities db = new ohjelmistodataEntities();

        // GET: Osaamisaihe
        public ActionResult Index()
        {
            return View(db.Osaamisaiheet.ToList());
        }

        // GET: Osaamisaihe/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamisaiheet osaamisaiheet = db.Osaamisaiheet.Find(id);
            if (osaamisaiheet == null)
            {
                return HttpNotFound();
            }
            return View(osaamisaiheet);
        }

        // GET: Osaamisaihe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Osaamisaihe/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OsaamisaiheID,Kuvaus")] Osaamisaiheet osaamisaiheet)
        {
            if (ModelState.IsValid)
            {
                db.Osaamisaiheet.Add(osaamisaiheet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(osaamisaiheet);
        }

        // GET: Osaamisaihe/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamisaiheet osaamisaiheet = db.Osaamisaiheet.Find(id);
            if (osaamisaiheet == null)
            {
                return HttpNotFound();
            }
            return View(osaamisaiheet);
        }

        // POST: Osaamisaihe/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OsaamisaiheID,Kuvaus")] Osaamisaiheet osaamisaiheet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(osaamisaiheet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(osaamisaiheet);
        }

        // GET: Osaamisaihe/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osaamisaiheet osaamisaiheet = db.Osaamisaiheet.Find(id);
            if (osaamisaiheet == null)
            {
                return HttpNotFound();
            }
            return View(osaamisaiheet);
        }

        // POST: Osaamisaihe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Osaamisaiheet osaamisaiheet = db.Osaamisaiheet.Find(id);
            db.Osaamisaiheet.Remove(osaamisaiheet);
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
                                                   where ho.OsaamisaiheID == id
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
