using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using BestellErfassung.DomainObjects;
using BestellErfassung.DomainObjects.Bestellungen;
using BestellErfassung.DomainObjects.Artikel;
using BestellErfassung.DomainObjects.Module.MassTool;
using BestellErfassung.DomainObjects.Tools;

namespace MasspackWebApi.Controllers
{
    [Authorize]
    public class BestellungAbsendenController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();

        public IHttpActionResult Get()
        {
            return Ok("You hit it!");
        }
        public IHttpActionResult Get(int Oid)
        {
            //create a list of BestellungSummen
            List<BestellungSummen> bestellungSummenList = new List<BestellungSummen>();
            decimal summe = 0;

            //Bestellung
            var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", Oid));

            //list of all bestellArtikels
            //var bestellArtikels = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellArtikel)), CriteriaOperator.Parse("Bestellung==?", bestellung), null, 10, null, true);
            var bestellArtikels = new XPCollection<BestellArtikel>(unitOfWork, CriteriaOperator.Parse("Bestellung==?", bestellung));

            foreach (BestellArtikel bestellArtikel in bestellArtikels)
            {
                Artikelstamm _artikelstamm = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", bestellArtikel.Artikel.Oid));
                //ArtikelLieferbar artikelLieferbar = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Artikel.Oid==?", _artikelstamm.Oid));
                //_artikelstamm.Stueckzahl = artikelLieferbar.StueckzahlLieferbar;

                summe = Convert.ToDecimal(unitOfWork.Evaluate<BestellArtikel>(CriteriaOperator.Parse("sum(Stueckzahl)"), CriteriaOperator.Parse("Bestellung.Status == false AND Bestellung.Fertig == true AND Artikel == ?", _artikelstamm)));
                if (summe > 0)
                {
                    BestellungSummen bestellungSummen = new BestellungSummen(unitOfWork)
                    {
                        StueckSumme = summe,
                        Artikel = _artikelstamm
                    };
                    decimal difference = bestellungSummen.Artikel.Bestand - bestellungSummen.StueckSumme;
                    if (difference < 0)
                    {
                        bestellungSummen.Lieferbar = false;
                    }
                    else
                        bestellungSummen.Lieferbar = true;
                    bestellungSummenList.Add(bestellungSummen);
                }
            }

            //bestellungSummenList is ready

            if (bestellungSummenList.Count != 0)
            {
                //var newbestellungSummenList = bestellungSummenList.Where(i => i.Lieferbar == false);
                MassBestellung _massbestellung;
                BestellAuftraege _bestellauftrag;
                SQLConnection _sqlconnection = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
                if (_sqlconnection.Mitarbeiter != 0)
                {
                    if (bestellung.Fertig == true && bestellung.Status == false)
                    {
                        int newQty = 0;
                        foreach (BestellArtikel bestellArtikel in bestellArtikels)
                        {
                            _massbestellung = new MassBestellung(externalUow);
                            _massbestellung.mboid = _sqlconnection.Mitarbeiter + DateTime.Now.ToString("ddMMyyyy") + bestellArtikel.BestellKunden.Bestellung.Oid.ToString() + bestellArtikel.BestellKunden.KDNr;
                            _massbestellung.ArtikelNr = bestellArtikel.ArtikelNr;
                            _massbestellung.BestellNr = bestellArtikel.BestellKunden.Bestellung.Oid;
                            _massbestellung.KDNr = bestellArtikel.BestellKunden.KDNr;
                            _massbestellung.Stueck = bestellArtikel.Stueckzahl;
                            _massbestellung.Mitarbeiter = _sqlconnection.Mitarbeiter.ToString();
                            _massbestellung.Datum = DateTime.Now.Date;
                            bestellArtikel.mboid = _sqlconnection.Mitarbeiter + DateTime.Now.ToString("ddMMyyyy") + bestellArtikel.BestellKunden.Bestellung.Oid.ToString() + bestellArtikel.BestellKunden.KDNr;
                            _massbestellung.Save();
                            bestellArtikel.Save();
                            bestellArtikel.BestellKunden.Bestellung.Save();
                            bestellArtikel.Bestellung.Status = true;
                            // Neue Bestellung für BestellAuftrag - Zuweisung
                            _bestellauftrag = new BestellAuftraege(unitOfWork);
                            _bestellauftrag.mboid = _massbestellung.mboid;
                            _bestellauftrag.Bestellung = bestellArtikel.Bestellung;

                            //update available quantity
                            //newQty = bestellArtikel.Artikel.Bestand - Convert.ToInt32(bestellArtikel.Stueckzahl);
                            //bestellArtikel.Artikel.Bestand = newQty;
                            //bestellArtikel.Artikel.Save();
                            externalUow.CommitChanges();
                            unitOfWork.CommitChanges();
                        }
                        bestellung.Fertig = true;
                        unitOfWork.CommitChanges();
                        return Json("Bestellungen erfolgreich abgesendet");
                    }
                    else
                    {
                        return Json("No Bistell to send");
                    }
                }
                else
                {
                    return Json("Bitte tragen Sie unter 'Einstellungen' einen Mitarbeiter ein");
                }


            }
            else
            {
                return Json("Es wurde keine Bestellung versendet");
            }

        }

    }
}
