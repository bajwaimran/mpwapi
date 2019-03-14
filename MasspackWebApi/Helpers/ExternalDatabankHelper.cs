using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using BestellErfassung.DomainObjects.Artikel;
using BestellErfassung.DomainObjects.Kunden;
using MasspackWebApi.Models;

namespace MasspackWebApi.Helpers
{
    public class ExternalDatabankHelper
    {
        private UnitOfWork externalUow = MasspackWebApi.XPO.MasterXpoHelper.GetNewUnitOfWork();
        private UnitOfWork unitOfWork = new UnitOfWork();
        public IEnumerable<Artikelstamm> GetArtikels()
        {
            var artikels = externalUow.Query<Artikelstamm>();
            return artikels.ToList();
        }
        public IEnumerable<Kundenstamm> GetKundens()
        {
            var kundens = externalUow.Query<Kundenstamm>();
            return kundens.ToList();
        }



        public void CopyKundenAndArtikels(MassOrder massOrder)
        {

            //var kundens = new XPCollection<Kundenstamm>(externalUow, CriteriaOperator.Parse("Oid==", massOrder.KundenIds));
            foreach (var item in massOrder.KundenIds)
            {
                var kunden = externalUow.FindObject<Kundenstamm>(CriteriaOperator.Parse("Oid==?", item));
                var check = unitOfWork.FindObject<Kundenstamm>(CriteriaOperator.Parse("KDNr==?", kunden.KDNr));
                if (check == null)
                {
                    Kundenstamm newKunden = new Kundenstamm(unitOfWork)
                    {
                        Oid = kunden.Oid,
                        Anrede = kunden.Anrede,
                        eMail = kunden.eMail,
                        kdauftrgesperrt = kunden.kdauftrgesperrt,
                        KDNr = kunden.KDNr,
                        Name1 = kunden.Name1,
                        Name2 = kunden.Name2,
                        Name3 = kunden.Name3,
                        Nation = kunden.Nation,
                        Ort = kunden.Ort,
                        PLZ = kunden.PLZ,
                        Selektion = kunden.Selektion,
                        Strasse = kunden.Strasse,
                        Suchname = kunden.Suchname,
                        Titel = kunden.Titel
                    };

                    unitOfWork.CommitChanges();
                }

            }


            foreach (var item in massOrder.ArtikelIds)
            {
                var artikel = externalUow.FindObject<Artikelstamm>(CriteriaOperator.Parse("Oid==?", item));
                var check = unitOfWork.FindObject<Artikelstamm>(CriteriaOperator.Parse("ArtNrInt==?", artikel.ArtNrInt));
                if (check == null)
                {
                    Artikelstamm newArtikel = new Artikelstamm(unitOfWork)
                    {
                        Oid = artikel.Oid,
                        Artikeltext1 = artikel.Artikeltext1,
                        Artikeltext2 = artikel.Artikeltext2,
                        Artikeltext3 = artikel.Artikeltext3,
                        Bestand = artikel.Bestand,
                        Bezeichnung = artikel.Bezeichnung,
                        Stueckzahl = artikel.Stueckzahl,
                        Verpackungseinheit = artikel.Verpackungseinheit,
                        ArtNr = artikel.ArtNr,
                        ArtNrInt = artikel.ArtNrInt
                    };
                    unitOfWork.CommitChanges();

                }

            }
        }

    }
}