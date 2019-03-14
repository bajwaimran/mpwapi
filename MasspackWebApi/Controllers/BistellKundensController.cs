using BestellErfassung.DomainObjects.Bestellungen;
using BestellErfassung.DomainObjects.Kunden;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MasspackWebApi.Models;
using System.Collections.Generic;
using System.Web.Http;
namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class BistellKundensController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BistellKundens/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BistellKundens
        public void Post()
        {
            var kunden = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", 7));
            var bestell = unitOfWork.FindObject<BestellErfassung.DomainObjects.Bestellung>(CriteriaOperator.Parse("Oid==?", 22));
            var bestellKunden = new BestellKunden(unitOfWork)
            {
                Kunde = kunden,
                KDNr = kunden.KDNr,
                //Bestellung = bestell
            };
            unitOfWork.CommitChanges();
            try
            {
                bestellKunden.Bestellung = bestell;
                unitOfWork.CommitChanges();
            }
            catch (System.Exception e)
            {

                throw;
            }

        }

        // PUT: api/BistellKundens/5
        public void Put(int id, MassOrder values)
        {
            var bestellung = unitOfWork.FindObject<BestellErfassung.DomainObjects.Bestellung>(CriteriaOperator.Parse("Oid==?", id));

        }

        // DELETE: api/BistellKundens/5
        public void Delete(int id)
        {
        }
    }
}
