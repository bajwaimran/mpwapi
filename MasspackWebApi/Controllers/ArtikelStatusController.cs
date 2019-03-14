using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using BestellErfassung.DomainObjects.Artikel;
using MasspackWebApi.Models;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class ArtikelStatusController : BaseXpoController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            ViewBag.Artikles = unitOfWork.Query<Artikelstamm>().ToList();
            var model = unitOfWork.Query<ArtikelLieferbar>();
            return PartialView("~/Views/ArtikelStatus/_GridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(ItemModel obj)
        {
            ViewBag.Artikles = unitOfWork.Query<Artikelstamm>().ToList();
            var model = unitOfWork.Query<ArtikelLieferbar>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    var artikel = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", obj.Artikel));
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
            return PartialView("~/Views/ArtikelStatus/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(ItemModel obj)
        {
            ViewBag.Artikles = unitOfWork.Query<Artikelstamm>().ToList();
            var model = unitOfWork.Query<ArtikelLieferbar>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    var item = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Oid==?", obj.Oid));
                    if (obj != null)
                    {
                        //item.Artikel = obj.Artikel;
                        item.StueckzahlLieferbar = obj.StueckzahlLieferbar;
                        item.Save();
                        unitOfWork.CommitChanges();
                    }
                    else
                        ViewData["EditError"] = "Something went wrong.";
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("~/Views/ArtikelStatus/_GridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Oid)
        {
            ViewBag.Artikles = unitOfWork.Query<Artikelstamm>().ToList();
            var model = unitOfWork.Query<ArtikelLieferbar>();
            if (Oid != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    var item = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Oid==?", Oid));
                    if (item != null)
                    {
                        unitOfWork.Delete(item);
                        unitOfWork.CommitChanges();
                    }
                    else
                        ViewData["EditError"] = "Unable to find the customer.";


                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("~/Views/ArtikelStatus/_GridViewPartial.cshtml", model);
        }
    }
}