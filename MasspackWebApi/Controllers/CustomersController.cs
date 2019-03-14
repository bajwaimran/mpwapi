using BestellErfassung.DomainObjects.Kunden;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using MasspackWebApi.Helpers;
using MasspackWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class CustomersController : BaseXpoController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();
        //public ActionResult GridViewPartial()
        //{
        //    var model = unitOfWork.Query<Kundenstamm>();
        //    return PartialView("_GridViewPartial", model.ToList());
        //}
        //List<Kundenstamm> kundenList = new List<Kundenstamm>();
        //public CustomersController()
        //{
        //    kundenList.Clear();
        //    ExternalDatabankHelper helper = new ExternalDatabankHelper();
        //    var kundens = helper.GetKundens().ToList();
        //    kundenList.AddRange(kundens);
        //}

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            ViewBag.KundenartenList = unitOfWork.Query<Kundenarten>().ToList();
            var model = unitOfWork.Query<Kundenstamm>();
            return PartialView("~/Views/Clients/_GridViewPartial.cshtml", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(CustomerModel obj)
        {
            ViewBag.KundenartenList = unitOfWork.Query<Kundenarten>().ToList();
            var model = unitOfWork.Query<Kundenstamm>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model
                    var kundenart = unitOfWork.FindObject<Kundenarten>(CriteriaOperator.Parse("Oid==?", obj.Kundenart));
                    new Kundenstamm(unitOfWork)
                    {
                        Selektion = obj.Selektion,
                        KDNr = obj.KDNr,
                        Suchname = obj.Suchname,
                        Anrede = obj.Anrede,
                        Titel = obj.Titel,
                        Name1 = obj.Name1,
                        Name2 = obj.Name2,
                        Name3 = obj.Name3,
                        Strasse = obj.Strasse,
                        Nation = obj.Nation,
                        PLZ = obj.PLZ,
                        Ort = obj.Ort,
                        eMail = obj.eMail,
                        Kundenart = kundenart
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
            return PartialView("~/Views/Clients/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(CustomerModel obj)
        {
            ViewBag.KundenartenList = unitOfWork.Query<Kundenarten>().ToList();
            var model = unitOfWork.Query<Kundenstamm>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    var item = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", obj.Oid));
                    var kundenart = unitOfWork.FindObject<Kundenarten>(CriteriaOperator.Parse("Oid==?", obj.Kundenart));
                    if (obj != null)
                    {
                        item.Selektion = obj.Selektion;
                        item.KDNr = obj.KDNr;
                        item.Suchname = obj.Suchname;
                        item.Anrede = obj.Anrede;
                        item.Titel = obj.Titel;
                        item.Name1 = obj.Name1;
                        item.Name2 = obj.Name2;
                        item.Name3 = obj.Name3;
                        item.Strasse = obj.Strasse;
                        item.Nation = obj.Nation;
                        item.PLZ = obj.PLZ;
                        item.Ort = obj.Ort;
                        item.eMail = obj.eMail;
                        item.kdauftrgesperrt = obj.kdauftrgesperrt;
                        item.Kundenart = kundenart;

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
            return PartialView("~/Views/Clients/_GridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Oid)
        {
            ViewBag.KundenartenList = unitOfWork.Query<Kundenarten>().ToList();
            var model = unitOfWork.Query<Kundenstamm>();
            if (Oid != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    var item = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", Oid));
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
            return PartialView("~/Views/Clients/_GridViewPartial.cshtml", model);
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartialOrder()
        {
            List<Kundenstamm> kundenList = new List<Kundenstamm>();
            SortingCollection sorting = new SortingCollection();
            sorting.Add(new SortProperty("Suchname", SortingDirection.Ascending));
            var kundens = externalUow.GetObjects(unitOfWork.GetClassInfo(typeof(Kundenstamm)), CriteriaOperator.Parse("kdauftrgesperrt == ?", false), sorting, 1000, false, false);
            ////ViewBag.Customers = unitOfWork.Query<BestellErfassung.DomainObjects.Kunden.Kundenstamm>().ToList();
            foreach (var item in kundens)
            {
                kundenList.Add(item as Kundenstamm);
            }
            //ViewBag.KundenartenList = unitOfWork.Query<Kundenarten>().ToList();
            var model = kundenList;
            ////var model = unitOfWork.Query<Kundenstamm>();


            return PartialView("_GridViewPartialOrder", kundens);
        }


        public ActionResult GridOne()
        {
            var model = unitOfWork.Query<Kundenstamm>().ToList();

            return PartialView("_GridOne", model);
        }
    }
}