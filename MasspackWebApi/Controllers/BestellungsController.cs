using BestellErfassung.DomainObjects;
using BestellErfassung.DomainObjects.Artikel;
using BestellErfassung.DomainObjects.Bestellungen;
using BestellErfassung.DomainObjects.Kunden;
using BestellErfassung.DomainObjects.Module.MassTool;
using BestellErfassung.DomainObjects.Tools;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using MasspackWebApi.Helpers;
using MasspackWebApi.Models;
using MasspackWebApi.XPO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace MasspackWebApi.Controllers
{

    [Authorize]
    public class BestellungsController : BaseXpoController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();
        //UnitOfWork externalUow = MasterXpoHelper.GetNewUnitOfWork();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GridViewPartial()
        {
            var model = unitOfWork.Query<BestellErfassung.DomainObjects.Bestellung>();
            return PartialView("_GridViewPartial", model);
        }
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(XpoModelBinder))]Bestellung item)
        {
            if (ModelState.IsValid)
            {
                item.Save();
                unitOfWork.CommitChanges();
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            var model = unitOfWork.Query<BestellErfassung.DomainObjects.Bestellung>();
            return PartialView("_GridViewPartial", model);
        }

        public ActionResult GridViewPartialBestellKunden()
        {
            var model = unitOfWork.Query<BestellKunden>();
            return PartialView("_GridViewPartialBestellKunden", model);
        }
        public ActionResult GridViewPartialBestellArtikel()
        {
            var model = unitOfWork.Query<BestellArtikel>();
            return PartialView("_GridViewPartialBestellArtikel", model);
        }
        public ActionResult GridViewPartialBestellAuftraege()
        {
            var model = unitOfWork.Query<BestellAuftraege>();
            return PartialView("_GridViewPartialBestellAuftraege", model);
        }
        public ActionResult GridViewPartialBestellungSummen()
        {
            var model = unitOfWork.Query<BestellAuftraege>();
            return PartialView("_GridViewPartialBestellungSummen", model);
        }

        //public ActionResult KundenPanel()
        //{
        //    if (!string.IsNullOrEmpty(Request.Params["kundenID"]))
        //    {
        //        int kundenID = int.Parse(Request.Params["kundenID"]);
        //        var kunden = unitOfWork.FindObject<BestellErfassung.DomainObjects.Kunden.Kundenstamm>(CriteriaOperator.Parse("Oid==?", kundenID));
        //        if (kunden != null)
        //        {
        //            new BestellKunden(unitOfWork)
        //            {
        //                Kunde = kunden,                        
        //            };
        //        }

        //    }

        //    int kundenOid = !string.IsNullOrEmpty(Request.Params["kundenID"]) ? int.Parse(Request.Params["kundenID"]) : unitOfWork.Query<BestellKunden>().First().Oid;
        //}
        public void Test()
        {

            var bestellung = new BestellErfassung.DomainObjects.Bestellung(unitOfWork)
            {
                //AuftragsNr = "",
                Datum = DateTime.Now,
                Fertig = true,
                Status = true,
                Zusatzangabe = ""
            };
            //var bestellAuftraege = new BestellAuftraege(unitOfWork)
            //{
            //    //generate new auftrage Nr
            //    AuftragsNr = "",
            //    Bestellung = bestellung,
            //    mboid = "" //generagte new mboid
            //};
            //bestellung.AuftragsNr = bestellAuftraege.AuftragsNr; // setting auftraege Nr  in bestellung

            var artikel = unitOfWork.FindObject<BestellErfassung.DomainObjects.Artikel.Artikelstamm>(CriteriaOperator.Parse("Oid==?", 1));
            var bestellKunden = new BestellKunden(unitOfWork)
            {
                Bestellung = bestellung,
                KDNr = 0
            };
            var bestellArtikel = new BestellArtikel(unitOfWork)
            {
                Artikel = artikel,
                ArtikelNr = artikel.ArtNr,
                Bestellung = bestellung,
                //AuftragsNrKW = 0,
                Bezeichnung = "",
                //mboid = bestellAuftraege.mboid,
                BestellKunden = bestellKunden,
                Stueckzahl = 0
            };
        }

        public ActionResult View(int id)
        {
            var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", id));
            if (bestellung != null)
            {
                return View(bestellung);
            }
            else
                return Content(string.Format("Unable to find your Bestellung. <a href='/Bestellungs/'>Click here</a> to see all Betellungs."));

        }
        List<BestellArtikel> bistellArtikelList;
        public ActionResult BistellArtiklesPartial()
        {
            if (!string.IsNullOrEmpty(Request.Params["Oid"]))
            {
                int Oid = int.Parse(Request.Params["Oid"]);
                Session["bistellKundenOid"] = Oid;
                var bestellKunden = unitOfWork.FindObject<BestellKunden>(CriteriaOperator.Parse("Oid==?", Oid));
                var model = bestellKunden.BestellKunden_BestellArtikel_XPColl;
                bistellArtikelList = model.ToList();
                Session["bistellArtikelList"] = model.ToList();
                return PartialView("BistellArtiklesPartial", model);
            }
            else
                return null;

        }

        public ActionResult GridCallBackPartial()
        {
            var model = (List<BestellArtikel>)Session["bistellArtikelList"];
            return PartialView("_GridCallBackPartial", model);
        }

        public ActionResult GridCallBackPartialUpdate([ModelBinder(typeof(XpoModelBinder))]BestellArtikel item)
        {
            if (ModelState.IsValid)
            {
                item.Save();
                unitOfWork.CommitChanges();
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            int bistellKundenOid = int.Parse(Session["bistellKundenOid"].ToString());
            var bestellKunden = unitOfWork.FindObject<BestellKunden>(CriteriaOperator.Parse("Oid==?", bistellKundenOid));
            var model = bestellKunden.BestellKunden_BestellArtikel_XPColl;
            return PartialView("_GridCallBackPartial", model);
        }
        public ActionResult GridCallBackPartialDelete(int Oid)
        {
            var item = unitOfWork.FindObject<BestellArtikel>(CriteriaOperator.Parse("Oid==?", Oid));
            if (item != null)
            {
                item.Delete();
                unitOfWork.CommitChanges();
            }

            int bistellKundenOid = int.Parse(Session["bistellKundenOid"].ToString());
            var bestellKunden = unitOfWork.FindObject<BestellKunden>(CriteriaOperator.Parse("Oid==?", bistellKundenOid));
            var model = bestellKunden.BestellKunden_BestellArtikel_XPColl;
            return PartialView("_GridCallBackPartial", model);
        }
        public List<BestellungSummen> BestellSummen(int Oid)
        {
            decimal summe = 0;
            List<BestellungSummen> bestellungSummenList = new List<BestellungSummen>();
            //check if stock is available
            //var bestellArtikels = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellArtikel)), CriteriaOperator.Parse("Oid==?", Oid), null, 10, null, true);
            var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", Oid));
            var bestellArtikels = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellArtikel)), CriteriaOperator.Parse("Bestellung==?", bestellung), null, 10, null, true);


            foreach (BestellArtikel bestellArtikel in bestellArtikels)
            {
                Artikelstamm _artikelstamm = externalUow.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", bestellArtikel.Artikel));
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

            unitOfWork.CommitChanges();
            return bestellungSummenList;

        }


        public string BestellAbsenden(int Oid)
        {
            List<BestellungSummen> bestellungSummenList = BestellSummen(Oid);
            //var xpCBestellungSummen = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellungSummen)), CriteriaOperator.Parse("Lieferbar==false"), null, 100, false, false);

            var xpCBestellungSummen = bestellungSummenList;
            //bestellungSummenList = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellungSummen)), CriteriaOperator.Parse("Lieferbar==false"), null, 100, false, false);
            string response = "";
            if (bestellungSummenList.Count != 0)
            {
                var newbestellungSummenList = bestellungSummenList.Where(i => i.Lieferbar == false);

                decimal count = newbestellungSummenList.Count();
                if (count == 0)
                {
                    MassBestellung _massbestellung;
                    BestellAuftraege _bestellauftrag;

                    SQLConnection _sqlconnection = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
                    if (_sqlconnection.Mitarbeiter != 0)
                    {
                        var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==? AND Fertig == true AND Status == false", Oid));
                        if (bestellung != null)
                        {

                            var bestellArtikels = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellArtikel)), CriteriaOperator.Parse("Bestellung ==? AND Bestellung.Status == false", bestellung.Oid), null, 100, false, false);
                            foreach (BestellArtikel bestellArtikel in bestellArtikels)
                            {
                                _massbestellung = new MassBestellung(unitOfWork);
                                _massbestellung.mboid = bestellArtikel.mboid;
                                _massbestellung.ArtikelNr = bestellArtikel.ArtikelNr;
                                _massbestellung.BestellNr = bestellArtikel.BestellKunden.Bestellung.Oid;
                                _massbestellung.KDNr = bestellArtikel.BestellKunden.KDNr;
                                _massbestellung.Stueck = bestellArtikel.Stueckzahl;
                                _massbestellung.Mitarbeiter = null;
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
                            }
                            bestellung.Fertig = true;
                            bestellung.Save();
                            unitOfWork.CommitChanges();
                            return response = "Bestellungen erfolgreich abgesendet";

                        }
                        else
                        {
                            return response = "No Bistell to send";
                        }
                        response = "No Bistell to send";
                    }
                    else
                    {
                        return response = "Bitte tragen Sie unter 'Einstellungen' einen Mitarbeiter ein";
                    }
                    var bestellungen = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(Bestellung)), CriteriaOperator.Parse("Fertig == true AND Status == false"), null, 10, false, false);

                    return response = "Bestellungen erfolgreich abgesendet";

                }
                else
                {
                    return response = "Nicht alle Artikel sind lieferbar! Bitte ändern Sie die Stückzahlen";
                }

            }
            else
            {
                return response = "Es wurde keine Bestellung versendet";
            }



        }

        public string Put(int Oid)
        {
            //create a list of BestellungSummen
            List<BestellungSummen> bestellungSummenList = new List<BestellungSummen>();
            decimal summe = 0;

            //Bestellung
            var bestellung = unitOfWork.FindObject<Bestellung>(CriteriaOperator.Parse("Oid==?", Oid));

            //list of all bestellArtikels
            var bestellArtikels = unitOfWork.GetObjects(unitOfWork.GetClassInfo(typeof(BestellArtikel)), CriteriaOperator.Parse("Bestellung==?", bestellung), null, 10, null, true);
            foreach (BestellArtikel bestellArtikel in bestellArtikels)
            {
                Artikelstamm _artikelstamm = externalUow.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", bestellArtikel.Artikel));
                ArtikelLieferbar artikelLieferbar = unitOfWork.FindObject<ArtikelLieferbar>(CriteriaOperator.Parse("Artikel.Oid==?", _artikelstamm.Oid));
                _artikelstamm.Stueckzahl = artikelLieferbar.StueckzahlLieferbar;
                _artikelstamm.Save();
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
                var newbestellungSummenList = bestellungSummenList.Where(i => i.Lieferbar == false);
                if (newbestellungSummenList.Count() == 0)
                {
                    MassBestellung _massbestellung;
                    BestellAuftraege _bestellauftrag;
                    SQLConnection _sqlconnection = unitOfWork.FindObject<SQLConnection>(CriteriaOperator.Parse("Oid==?", 1));
                    if (_sqlconnection.Mitarbeiter != 0)
                    {
                        if (bestellung.Fertig == true && bestellung.Status == false)
                        {
                            foreach (BestellArtikel bestellArtikel in bestellArtikels)
                            {
                                _massbestellung = new MassBestellung(unitOfWork);
                                _massbestellung.mboid = _sqlconnection.Mitarbeiter + DateTime.Now.ToString("ddMMyyyy") + bestellArtikel.BestellKunden.Bestellung.Oid.ToString() + bestellArtikel.BestellKunden.KDNr;
                                _massbestellung.ArtikelNr = bestellArtikel.ArtikelNr;
                                _massbestellung.BestellNr = bestellArtikel.BestellKunden.Bestellung.Oid;
                                _massbestellung.KDNr = bestellArtikel.BestellKunden.KDNr;
                                _massbestellung.Stueck = bestellArtikel.Stueckzahl;
                                _massbestellung.Mitarbeiter = null;
                                _massbestellung.Datum = DateTime.Now.Date;
                                bestellArtikel.mboid = _sqlconnection.Mitarbeiter + DateTime.Now.ToString("ddMMyyyy") + bestellArtikel.BestellKunden.Bestellung.Oid.ToString() + bestellArtikel.BestellKunden.KDNr;
                                _massbestellung.Save();
                                //try
                                //{
                                //    externalUow.CommitChanges();
                                //}
                                //catch (Exception e)
                                //{

                                //    throw;
                                //}
                                bestellArtikel.Save();
                                bestellArtikel.BestellKunden.Bestellung.Save();
                                bestellArtikel.Bestellung.Status = true;
                                // Neue Bestellung für BestellAuftrag - Zuweisung
                                _bestellauftrag = new BestellAuftraege(unitOfWork);
                                _bestellauftrag.mboid = _massbestellung.mboid;
                                _bestellauftrag.Bestellung = bestellArtikel.Bestellung;
                            }
                            bestellung.Fertig = true;
                            bestellung.Save();
                            unitOfWork.CommitChanges();
                            //return Content("Bestellungen erfolgreich abgesendet");
                            return "Bestellungen erfolgreich abgesendet";
                        }
                        else
                        {
                            //return Content("No Bistell to send");
                            return "No Bistell to send";
                        }
                    }
                    else
                    {
                        //return Content("Bitte tragen Sie unter 'Einstellungen' einen Mitarbeiter ein");
                        return "Bitte tragen Sie unter 'Einstellungen' einen Mitarbeiter ein";
                    }
                }
                else
                {
                    //return Content("Nicht alle Artikel sind lieferbar! Bitte ändern Sie die Stückzahlen");
                    return "Nicht alle Artikel sind lieferbar! Bitte ändern Sie die Stückzahlen";
                }

            }
            else
            {
                //return Content("Es wurde keine Bestellung versendet");
                return "Es wurde keine Bestellung versendet";
            }

        }

        [HttpPost]
        [Route("Add")]
        public ActionResult CreateNewBestellung(MassOrder massOrder)
        {
            //CopyKundenAndArtikels(massOrder);
            return null;
        }


    }
}