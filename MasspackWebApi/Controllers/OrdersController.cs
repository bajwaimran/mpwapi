using BestellErfassung.DomainObjects;
using BestellErfassung.DomainObjects.Artikel;
using BestellErfassung.DomainObjects.Kunden;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MasspackWebApi.Helpers;
using MasspackWebApi.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();

        // GET: api/Orders
        public IHttpActionResult Get()
        {

            List<Order> list = new List<Order>();
            var model = unitOfWork.Query<Bestellung>();
            foreach (Bestellung obj in model)
            {

                list.Add(Mapper.GetOrder(obj));
            }
            return Ok(list);
        }

        // GET: api/Orders/5
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", id));
            if (bestellung != null)
            {
                return Ok(Mapper.GetOrder(bestellung));
            }
            else
                return NotFound();

        }

        // POST: api/Orders
        /* {"Zusatzangabe":"notes for order", "KundenIds": [1,2,4], "ArtikelIds" : [1001,1002,2001] } */
        public IHttpActionResult Post(MassOrder model)
        {
            ExternalDatabankHelper helper = new ExternalDatabankHelper();
            helper.CopyKundenAndArtikels(model);
            //createnew order and get the order
            var order = new Bestellung(unitOfWork)
            {
                Datum = DateTime.Now,
                Fertig = false,
                Status = false,
                Zusatzangabe = model.Zusatzangabe
            };
            try
            {
                unitOfWork.CommitChanges();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

            List<BestellErfassung.DomainObjects.Artikel.Artikelstamm> artikelList = new List<BestellErfassung.DomainObjects.Artikel.Artikelstamm>();
            foreach (var artikelId in model.Items)
            {
                var externalArtikel = externalUow.FindObject<BestellErfassung.DomainObjects.Artikel.Artikelstamm>(CriteriaOperator.Parse("Oid==?", artikelId.Oid));
                var artikel = unitOfWork.FindObject<BestellErfassung.DomainObjects.Artikel.Artikelstamm>(CriteriaOperator.Parse("ArtNr==?", externalArtikel.ArtNr));
                artikelList.Add(artikel);
            }
            List<Item> list = new List<Item>();
            foreach (var item in model.Items)
            {
                list.Add(item);
            }


            List<BestellErfassung.DomainObjects.Bestellungen.BestellKunden> bestellKundenList = new List<BestellErfassung.DomainObjects.Bestellungen.BestellKunden>();
            foreach (var kundenId in model.KundenIds)
            {
                var externalKunden = externalUow.FindObject<BestellErfassung.DomainObjects.Kunden.Kundenstamm>(CriteriaOperator.Parse("Oid==?", kundenId));
                var kunden = unitOfWork.FindObject<BestellErfassung.DomainObjects.Kunden.Kundenstamm>(CriteriaOperator.Parse("KDNr==?", externalKunden.KDNr));

                var bestellKunden = new BestellErfassung.DomainObjects.Bestellungen.BestellKunden(unitOfWork)
                {
                    Kunde = kunden,
                    KDNr = kunden.KDNr,
                    Bestellung = order
                };
                unitOfWork.CommitChanges();
                bestellKundenList.Add(bestellKunden);

            }
            foreach (var bestellKunden in bestellKundenList)
            {
                var conn = unitOfWork.FindObject<BestellErfassung.DomainObjects.Tools.SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
                string mboid = conn.Mitarbeiter + DateTime.Now.ToString("ddMMyyyy") + order.Oid + bestellKunden.KDNr;

                foreach (var artikel in artikelList)
                {
                    int stuckzahl = list.Find(i => i.ArtNr == artikel.ArtNr).Quantity;
                    bestellKunden.BestellKunden_BestellArtikel_XPColl.Add(new BestellErfassung.DomainObjects.Bestellungen.BestellArtikel(unitOfWork)
                    {
                        Artikel = artikel,
                        ArtikelNr = artikel.ArtNr,
                        Bestellung = order,
                        Bezeichnung = artikel.Bezeichnung,
                        Stueckzahl = stuckzahl
                    });
                }
                unitOfWork.CommitChanges();
            }



            return Ok(order.Oid);





        }
        // PUT: api/Orders/5
        public bool Put(int id, Order item)
        {
            var order = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", id));
            if (order != null)
            {
                if (!string.IsNullOrEmpty(item.Fertig.ToString()))
                {
                    order.Fertig = item.Fertig;
                    order.Save();
                    unitOfWork.CommitChanges();

                }
                return order.Fertig;

            }
            else
            {
                return false;
            }
        }

        // DELETE: api/Orders/5
        public void Delete(int id)
        {
        }


    }
}
