using BestellErfassung.DomainObjects.Artikel;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MasspackWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class ItemsController : BaseXpoController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();
        public IEnumerable<Art> GetAll()
        {
            var model = (from i in unitOfWork.Query<Artikelstamm>()
                         join bc in unitOfWork.Query<ArtikelLieferbar>() on i.Oid equals bc.Artikel.Oid
                         select new Art()
                         {
                             Oid = i.Oid,
                             Artikeltext1 = i.Artikeltext1,
                             Artikeltext2 = i.Artikeltext2,
                             Artikeltext3 = i.Artikeltext3,
                             ArtNr = i.ArtNrInt,
                             Bestand = i.Bestand,
                             Bezeichnung = i.Bezeichnung,
                             Stueckzahl = bc.StueckzahlLieferbar,
                             Verpackungseinheit = i.Verpackungseinheit,
                             StueckzahlLieferbar = bc.StueckzahlLieferbar
                         }).ToList();
            return model;
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = GetAll();
            return PartialView("_GridViewPartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Art obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    var artikel = new Artikelstamm(unitOfWork)
                    {
                        ArtNr = obj.ArtNr.ToString(),
                        ArtNrInt = obj.ArtNr,
                        Artikeltext1 = obj.Artikeltext1,
                        Artikeltext2 = obj.Artikeltext2,
                        Artikeltext3 = obj.Artikeltext3,
                        Bestand = obj.Bestand,
                        Bezeichnung = obj.Bezeichnung,
                        Stueckzahl = obj.Stueckzahl,
                        Verpackungseinheit = obj.Verpackungseinheit
                    };
                    new ArtikelLieferbar(unitOfWork)
                    {
                        Artikel = artikel,
                        StueckzahlLieferbar = obj.StueckzahlLieferbar
                    };
                    unitOfWork.CommitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = GetAll();
            return PartialView("~/Views/Items/_GridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Art obj)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    var artikelLieferbar = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Artikel.Oid==?", obj.Oid));
                    if (artikelLieferbar != null)
                    {

                        artikelLieferbar.Artikel.Artikeltext1 = obj.Artikeltext1;
                        artikelLieferbar.Artikel.Artikeltext2 = obj.Artikeltext2;
                        artikelLieferbar.Artikel.Artikeltext3 = obj.Artikeltext3;
                        artikelLieferbar.Artikel.Bestand = obj.Bestand;
                        artikelLieferbar.Artikel.Bezeichnung = obj.Bezeichnung;
                        artikelLieferbar.Artikel.Stueckzahl = obj.Stueckzahl;
                        artikelLieferbar.Artikel.Verpackungseinheit = obj.Verpackungseinheit;
                        artikelLieferbar.Artikel.Save();

                        artikelLieferbar.StueckzahlLieferbar = obj.StueckzahlLieferbar;
                        artikelLieferbar.Save();

                        unitOfWork.CommitChanges();
                    }
                    else
                        ViewData["EditError"] = "Unable to find the artikel";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";

            var model = GetAll();
            return PartialView("~/Views/Items/_GridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Oid)
        {

            if (Oid != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    var item = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", Oid));
                    unitOfWork.Delete(item);
                    var lieferbar = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Artikel.Oid==?", Oid));
                    unitOfWork.Delete(lieferbar);
                    unitOfWork.CommitChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }

            var model = GetAll();
            return PartialView("~/Views/Items/_GridViewPartial.cshtml", model);
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartialOrder()
        {
            var model = externalUow.Query<Artikelstamm>().ToList();

            return PartialView("_GridViewPartialOrder", model);
        }

    }
}