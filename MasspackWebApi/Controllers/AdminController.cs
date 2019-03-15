using DevExpress.Xpo;
using MasspackWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Data.Filtering;

namespace MasspackWebApi.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : BaseXpoController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Tutorials()
        {
            return View();
        }
        public ActionResult GridViewPartial()
        {
            ViewBag.TutorialTypes = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            var model = new XPCollection<Tutorial>(unitOfWork, true).ToList();
            return PartialView("_GridViewPartial", model);
        }

        public ActionResult TutorialTypePartial()
        {
            var model = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            return PartialView("_TutorialTypePartial", model);
        }
        //[AllowAnonymous]
        //public string Update(Tut tutorial)
        //{
        //    if (tutorial.TutorialType == "NeuBestellung")
        //    {
        //        var item = unitOfWork.FindObject<Tutorial>(CriteriaOperator.Parse("[TutorialType] = ?", "NeuBestellung"));
        //        if (item == null)
        //        {
        //            new Tutorial(unitOfWork)
        //            {
        //                TutorialType = "NeuBestellung",
        //                ButtonText = tutorial.ButtonText,
        //                Heading = tutorial.Heading,
        //                Text = tutorial.Text,
        //                Timestamp = DateTime.Now
        //            };
        //            unitOfWork.CommitChanges();
        //        }
        //        else
        //        {
        //            item.ButtonText = tutorial.ButtonText;
        //            item.Heading = tutorial.Heading;
        //            item.Text = tutorial.Text;
        //            item.Timestamp = DateTime.Now;
        //            unitOfWork.CommitChanges();
        //        }
        //        return "updated";
        //    }
        //    if (tutorial.TutorialType == "ViewBestellung")
        //    {
        //        var item = unitOfWork.FindObject<Tutorial>(CriteriaOperator.Parse("TutorialType == ViewBestellung"));
        //        if (item == null)
        //        {
        //            new Tutorial(unitOfWork)
        //            {
        //                TutorialType = "ViewBestellung",
        //                ButtonText = tutorial.ButtonText,
        //                Heading = tutorial.Heading,
        //                Text = tutorial.Text,
        //                Timestamp = DateTime.Now
        //            };
        //            unitOfWork.CommitChanges();
        //        }
        //        else
        //        {
        //            item.ButtonText = tutorial.ButtonText;
        //            item.Heading = tutorial.Heading;
        //            item.Text = tutorial.Text;
        //            item.Timestamp = DateTime.Now;
        //            unitOfWork.CommitChanges();
        //        }
        //        return "updated";
        //    }
        //    else
        //    {
        //        return "Not Found";
        //    }
        //}


        [HttpPost, ValidateInput(false)]
        public ActionResult TutorialTypeAddNew([ModelBinder(typeof(XpoModelBinder))]TutorialType item)
        {
            if (!ModelState.IsValid)
            {
                ViewData["EditError"] = "Correct all errors.";
            }
            else
            {
                new TutorialType(unitOfWork)
                {
                    Name = item.Name
                };
                unitOfWork.CommitChanges();
            }
            var model = new XPCollection<TutorialType>(unitOfWork, true);
            return PartialView("_TutorialTypePartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TutorialTypeUpdate([ModelBinder(typeof(XpoModelBinder))]TutorialType item)
        {
            if (!ModelState.IsValid)
            {
                ViewData["EditError"] = "Correct all errors.";
            }
            else
            {
                item.Save();
                unitOfWork.CommitChanges();
            }
            var model = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            return PartialView("_TutorialTypePartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult TutorialTypeDelete(System.String Oid)
        {
            var item = unitOfWork.FindObject<TutorialType>(CriteriaOperator.Parse("Oid==?", Oid));
            if (item != null)
            {
                unitOfWork.Delete(item);
                unitOfWork.CommitChanges();
            }
            var model = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            return PartialView("_TutorialTypePartial", model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(Tut item)
        {
            if (!ModelState.IsValid)
            {
                ViewData["EditError"] = "Correct all errors.";
            }
            else
            {
                var tutorialType = unitOfWork.FindObject<TutorialType>(CriteriaOperator.Parse("Oid==?", item.TutorialType));
                new Tutorial(unitOfWork)
                {
                    TutorialType = tutorialType,
                    ButtonText = item.ButtonText,
                    Heading = item.Heading,
                    Text = item.Text,
                    Timestamp = DateTime.Now
                };
                unitOfWork.CommitChanges();
            }
            ViewBag.TutorialTypes = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            var model = new XPCollection<Tutorial>(unitOfWork, true).ToList();
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(Tut item)
        {
            if (!ModelState.IsValid)
            {
                ViewData["EditError"] = "Correct all errors.";
            }
            else
            {
                var tutorial = unitOfWork.FindObject<Tutorial>(CriteriaOperator.Parse("Oid==?", item.Oid));
                tutorial.Text = item.Text;
                tutorial.Heading = item.Heading;
                unitOfWork.CommitChanges();
            }
            ViewBag.TutorialTypes = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            var model = new XPCollection<Tutorial>(unitOfWork, true).ToList();
            return PartialView("_GridViewPartial", model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.String Oid)
        {
            var item = unitOfWork.FindObject<Tutorial>(CriteriaOperator.Parse("Oid==?", Oid));
            if (item != null)
            {
                unitOfWork.Delete(item);
                unitOfWork.CommitChanges();
            }
            ViewBag.TutorialTypes = new XPCollection<TutorialType>(unitOfWork, true).ToList();
            var model = new XPCollection<Tutorial>(unitOfWork, true).ToList();
            return PartialView("_GridViewPartial", model);
        }


    }

}