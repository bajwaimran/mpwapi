using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevExpress.Xpo;
using MasspackWebApi.Models;
using BestellErfassung.DomainObjects.Kunden;
using DevExpress.Data.Filtering;
using System.Web.Mvc;

namespace MasspackWebApi.Controllers
{
    [System.Web.Http.Authorize]
    public class KundensController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        // GET: api/Kundens
        public IHttpActionResult Get()
        {
            List<Customer> list = new List<Customer>();
            var model = unitOfWork.Query<Kundenstamm>();
            foreach (var obj in model)
            {

                list.Add(new Customer
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
                    Kundenart = obj.Kundenart.Oid,
                    kdauftrgesperrt = obj.kdauftrgesperrt
                });
            }
            var result = new HttpStatusCodeResult(201);
            return Ok(list.ToArray());
        }

        // GET: api/Kundens/5
        public IHttpActionResult Get(int id)
        {
            var obj = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", id));
            if (obj != null)
            {
                var item = new Customer
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
                    Kundenart = obj.Kundenart.Oid,
                    kdauftrgesperrt = obj.kdauftrgesperrt
                };
                return Ok(item);
            }
            else
                return NotFound();
        }

        // POST: api/Kundens
        public IHttpActionResult Post(Customer obj)
        {
            if (ModelState.IsValid)
            {
                //new Kundenarten(unitOfWork)
                //{
                //    Kundenart = "kundenarten"
                //};
                unitOfWork.CommitChanges();
                var kunderart = unitOfWork.FindObject<Kundenarten>(CriteriaOperator.Parse("Oid==?", obj.Kundenart));
                var item = new Kundenstamm(unitOfWork)
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
                    Kundenart = kunderart,

                    kdauftrgesperrt = obj.kdauftrgesperrt
                    /**/
                };
                try
                {
                    unitOfWork.CommitChanges();
                    return Ok(item.Oid);
                }
                catch (Exception e)
                {
                    return Ok(e.Message);
                }


            }
            else
                return Ok("Correct All errror");

        }

        // PUT: api/Kundens/5
        public IHttpActionResult Put(int id, Customer obj)
        {
            if (ModelState.IsValid)
            {
                var item = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", id));
                if (item != null)
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

                    item.Save();
                    unitOfWork.CommitChanges();
                    return Ok(obj);
                }
                else
                    return NotFound();
            }
            else
                return NotFound();
        }

        // DELETE: api/Kundens/5
        public IHttpActionResult Delete(int id)
        {
            var item = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", id));
            if (item != null)
            {
                unitOfWork.Delete(item);
                unitOfWork.CommitChanges();
                return Ok("Deleted");
            }
            else
                return NotFound();

        }
    }
}
