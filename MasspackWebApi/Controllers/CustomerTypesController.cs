using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using BestellErfassung.DomainObjects.Kunden;
using MasspackWebApi.Models;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class CustomerTypesController : BaseXpoController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = unitOfWork.Query<Kundenarten>();
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(XpoModelBinder))] Kundenarten obj)
        {
            var model = unitOfWork.Query<Kundenarten>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to insert the new item in your model

                    new Kundenarten(unitOfWork)
                    {
                        Kundenart = obj.Kundenart
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
            return PartialView("~/Views/CustomerTypes/_GridViewPartial.cshtml", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(XpoModelBinder))] Kundenarten obj)
        {
            var model = unitOfWork.Query<Kundenarten>();
            if (ModelState.IsValid)
            {
                try
                {
                    // Insert here a code to update the item in your model
                    var item = unitOfWork.FindObject<Kundenarten>(CriteriaOperator.Parse("Oid==?", obj.Oid));
                    if (obj != null)
                    {
                        item.Kundenart = obj.Kundenart;
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
            return PartialView("~/Views/CustomerTypes/_GridViewPartial.cshtml", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Oid)
        {
            var model = unitOfWork.Query<Kundenarten>();
            if (Oid != null)
            {
                try
                {
                    // Insert here a code to delete the item from your model
                    var item = unitOfWork.FindObject<Kundenarten>(CriteriaOperator.Parse("Oid==?", Oid));
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
            return PartialView("~/Views/CustomerTypes/_GridViewPartial.cshtml", model);
        }
    }
}