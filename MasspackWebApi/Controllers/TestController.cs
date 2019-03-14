using BestellErfassung.DomainObjects.Artikel;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();
        private UnitOfWork unitOfWork = new UnitOfWork();

        public ActionResult Get()
        {
            var model = externalUow.Query<BestellErfassung.DomainObjects.Kunden.Kundenstamm>();
            var model1 = new XPCollection<BestellErfassung.DomainObjects.Kunden.Kundenstamm>(externalUow, CriteriaOperator.Parse("kdauftrgesperrt == ?", false));
            return Content(model1.Count().ToString());
        }
        public bool LocalDbTest()
        {
            new BestellErfassung.DomainObjects.Bestellung(unitOfWork)
            {
                Datum = DateTime.Now,
                Fertig = false,
            };
            try
            {
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public bool TestCreateBestellkunden()
        {

            new BestellErfassung.DomainObjects.Bestellungen.BestellKunden(unitOfWork)
            {
                KDNr = 125
            };
            try
            {
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
        public bool TestCreateBestellArtikel()
        {

            new BestellErfassung.DomainObjects.Bestellungen.BestellArtikel(unitOfWork)
            {
                ArtikelNr = "1001"
            };
            try
            {
                unitOfWork.CommitChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }

        }
    }
}