using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BestellErfassung.DomainObjects.Artikel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using MasspackWebApi.Models;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class ArtikelController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: api/Artikel
        ItemsController controller = new ItemsController();
        List<Art> _artikelList;
        public ArtikelController()
        {

        }
        public ArtikelController(List<Art> artikelList)
        {
            _artikelList = artikelList;
        }
        public IHttpActionResult Get()
        {
            var model = controller.GetAll();
            return Ok(model);
        }

        // GET: api/Artikel/5
        public IHttpActionResult Get(int id)
        {
            var model = controller.GetAll();
            var artikel = model.Where(i => i.Oid == id);
            if (artikel.Count() == 0)
                return NotFound();
            return Ok(artikel);
        }

        // POST: api/Artikel

        public IHttpActionResult Post(Art item)
        {
            if (ModelState.IsValid)
            {
                var artikel = new Artikelstamm(unitOfWork)
                {
                    ArtNrInt = item.ArtNr,
                    ArtNr = item.ArtNr.ToString(),
                    Bestand = item.Bestand,
                    Bezeichnung = item.Bezeichnung,
                    Artikeltext1 = item.Artikeltext1,
                    Artikeltext2 = item.Artikeltext2,
                    Artikeltext3 = item.Artikeltext3,
                    Stueckzahl = item.Stueckzahl,
                    Verpackungseinheit = item.Verpackungseinheit
                };
                artikel.Save();
                new ArtikelLieferbar(unitOfWork)
                {
                    Artikel = artikel,
                    StueckzahlLieferbar = item.StueckzahlLieferbar
                };
                unitOfWork.CommitChanges();
                item.Oid = artikel.Oid;
                return Ok(item);
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Artikel/5
        public IHttpActionResult Put(int id, Art item)
        {

            var artikel = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", id));
            var artikelLieferbar = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Artikel==?", artikel));
            if (artikelLieferbar != null && artikel != null)
            {
                if (!string.IsNullOrEmpty(item.Artikeltext1))
                    artikelLieferbar.Artikel.Artikeltext1 = item.Artikeltext1;
                if (!string.IsNullOrEmpty(item.Artikeltext2))
                    artikelLieferbar.Artikel.Artikeltext2 = item.Artikeltext2;
                if (!string.IsNullOrEmpty(item.Artikeltext3))
                    artikelLieferbar.Artikel.Artikeltext3 = item.Artikeltext3;
                if (!string.IsNullOrEmpty(item.Bestand.ToString()))
                    artikelLieferbar.Artikel.Bestand = item.Bestand;
                if (!string.IsNullOrEmpty(item.Bezeichnung))
                    artikelLieferbar.Artikel.Bezeichnung = item.Bezeichnung;
                if (!string.IsNullOrEmpty(item.Verpackungseinheit.ToString()))
                    artikelLieferbar.Artikel.Verpackungseinheit = item.Verpackungseinheit;
                artikelLieferbar.Artikel.Save();
                if (!string.IsNullOrEmpty(item.StueckzahlLieferbar.ToString()))
                    artikelLieferbar.StueckzahlLieferbar = item.StueckzahlLieferbar;
                artikelLieferbar.Save();
                unitOfWork.CommitChanges();
                return Json(item);
            }
            else
                return NotFound();
        }

        // DELETE: api/Artikel/5
        public IHttpActionResult Delete(int id)
        {
            var item = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", id));
            if (item != null)
            {
                //unitOfWork.Delete(item);
                //unitOfWork.CommitChanges();
                return Ok("Deleted");
            }
            else
                return NotFound();
        }
    }
}
