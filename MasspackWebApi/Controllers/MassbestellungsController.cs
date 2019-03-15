using BestellErfassung.DomainObjects.Module.MassTool;
using DevExpress.Xpo;
using MasspackWebApi.XPO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    public class MassbestellungsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasterXpoHelper.GetNewUnitOfWork();
        public ActionResult Index()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = new XPCollection<MassBestellung>(externalUow, true);
            return PartialView("_GridViewPartial", model.ToList());
        }
    }
}