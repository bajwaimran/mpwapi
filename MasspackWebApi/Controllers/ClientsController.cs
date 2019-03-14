using BestellErfassung.DomainObjects.Kunden;
using BestellErfassung.DomainObjects.Tools;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Clients
        public ActionResult Index()
        {
            List<Kundenstamm> kundenList = new List<Kundenstamm>();
            SortingCollection sorting = new SortingCollection();
            sorting.Add(new SortProperty("Suchname", SortingDirection.Ascending));
            var kundens = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(Kundenstamm)), CriteriaOperator.Parse("kdauftrgesperrt == ?", false), sorting, 1000, false, false);
            //ViewBag.Customers = unitOfWork.Query<BestellErfassung.DomainObjects.Kunden.Kundenstamm>().ToList();
            foreach (var item in kundens)
            {
                kundenList.Add(item as Kundenstamm);
            }
            ViewBag.Customers = kundenList;
            ViewBag.Items = unitOfWork.Query<BestellErfassung.DomainObjects.Artikel.ArtikelLieferbar>().ToList();
            return View();
        }
        public ActionResult CustomerManagement()
        {
            return View();
        }
        public ActionResult ArtikelManagement()
        {
            return View();
        }
        public ActionResult CustomerTypes()
        {
            return View();
        }
        public ActionResult ArtikelStatus()
        {
            return View();
        }

        public ActionResult ArtikelDetails()
        {
            var model = unitOfWork.Query<BestellErfassung.DomainObjects.Bestellungen.BestellArtikel>();
            return PartialView("_ArtikelDetails", model);
        }
        public ActionResult Einstellungen()
        {
            var model = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
            return PartialView(model);
        }
    }
}