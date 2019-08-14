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
    public class HenkiloController : Controller
    {
        private ohjelmistodataEntities db = new ohjelmistodataEntities();

        // GET: Henkilo
        //public ActionResult Index()
        //{
        //    ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi");
        //    var henkilot = db.Henkilot.Include(h => h.HenkilonOsaamiset);
        //    return View(henkilot.ToList());
        //}
        public ActionResult Index()
        {
            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi");
            return View(db.Henkilot.ToList());
        }

        [HttpPost]
        public ActionResult Index(int HenkiloID)
        {
            ViewBag.HenkiloID = new SelectList(db.Henkilot, "HenkiloID", "Sukunimi");
            var henkilot = db.Henkilot.Include(s => s.Sukunimi).Where(a => a.HenkiloID == HenkiloID);
            return View(henkilot.ToList());
        }

   

     

        // GET: Henkilo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // GET: Henkilo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Henkilo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HenkiloID,Etunimi,Sukunimi,TyoPuhelin,TyoSahkoposti,Organiaatio")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Henkilot.Add(henkilot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(henkilot);
        }

        // GET: Henkilo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HenkiloID,Etunimi,Sukunimi,TyoPuhelin,TyoSahkoposti,Organiaatio")] Henkilot henkilot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(henkilot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(henkilot);
        }

        // GET: Henkilo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Henkilot henkilot = db.Henkilot.Find(id);
            if (henkilot == null)
            {
                return HttpNotFound();
            }
            return View(henkilot);
        }

        // POST: Henkilo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Henkilot henkilot = db.Henkilot.Find(id);
            db.Henkilot.Remove(henkilot);
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

        //public ActionResult Skill(string searching)
        //{
        //    List<SimplyOsaamisrekisteriData> model = new List<SimplyOsaamisrekisteriData>();

        //    ohjelmistodataEntities entities = new ohjelmistodataEntities();

        //    try
        //    {
        //        List<Henkilot> henkilot = entities.Henkilot.OrderBy(Henkilot => Henkilot.Sukunimi).ToList();

        //        // muodostetaan näkymämalli tietokannan rivien pohjalta       
        //        foreach (Henkilot hlo in henkilot)
        //        {
        //            SimplyOsaamisrekisteriData view = new SimplyOsaamisrekisteriData();
        //            view.HenkiloID = hlo.HenkiloID;
        //            view.Etunimi = hlo.Etunimi;
        //            view.Sukunimi = hlo.Sukunimi;
        //            view.TyoPuhelin = hlo.TyoPuhelin;
        //            view.TyoSahkoposti = hlo.TyoSahkoposti;
        //            view.Organiaatio = hlo.Organiaatio;
        //            view.Henkilonumero = hlo.Henkilonumero;

        //            view.HenkilonOsaamisID = hlo.HenkilonOsaamiset?.FirstOrDefault()?.HenkilonOsaamisID;
        //            view.Osaamistaso = hlo.HenkilonOsaamiset?.FirstOrDefault()?.Osaamistaso;

        //            SelectList list = new SelectList(henkilot, "HenkiloID", "Sukunimi");
        //            ViewBag.Henkilolistaus = list;
        //            model.Add(view);
        //        }
        //    }
        //    finally
        //    {
        //        entities.Dispose();
        //    }
        //    return View(model);
        //}//Index


        public ActionResult GetOsaamisRekisteri(int? id)
        {
            ohjelmistodataEntities entities = new ohjelmistodataEntities();

            List<HenkilonOsaamiset> hlosaamiset = (from ho in entities.HenkilonOsaamiset
                                                   where ho.HenkiloID == id
                                                   select ho).ToList();

            List<SimplyOsaamisrekisteriData> result = new List<SimplyOsaamisrekisteriData>();

            StringBuilder html = new StringBuilder();

            html.AppendLine("<td colspan=\"6\">" +
                                "<table id='osaamiset' class=\"table table-striped\">" +
                                " <th>  </ th>" +
                                " <th> Henkilon OsaamisID </ th>" +
                                " <th> Henkilönumero </ th>" +
                                " <th> Organisaatio </ th>" +
                                " <th> Osaamistaso </ th>" +
                                " <th> Osaamiset </ th>");

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
                data.TyoSahkoposti = henkilot[0].TyoSahkoposti;
                data.Organiaatio = henkilot[0].Organiaatio;

                List<Osaamisaiheet> osaamisaihe = (from o in entities.Osaamisaiheet
                                                   where o.OsaamisaiheID == hlosa.OsaamisaiheID
                                                   select o).ToList();

                data.Kuvaus = osaamisaihe[0].Kuvaus;

                html.AppendLine("</table></td>");

                result.Add(data);
            }

            entities.Dispose();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
