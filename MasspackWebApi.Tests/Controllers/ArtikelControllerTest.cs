using MasspackWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
//using System.Web.Http;
using MasspackWebApi.Models;


namespace MasspackWebApi.Tests.Controllers
{
    [TestClass]
    class ArtikelControllerTest
    {
        [TestMethod]
        public void GetAllArtikels_ShouldReturnArtikels()
        {
            var testArtikels = GetTestArts();
            var controller = new ArtikelController(testArtikels);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new System.Web.Http.HttpConfiguration();

            var response = controller.Get(1);

            //Assert

            Art art = testArtikels.Find(i => i.Oid == 1);
            Assert.AreEqual(response, art);
        }
        private List<Art> GetTestArts()
        {
            var testProducts = new List<Art>();
            testProducts.Add(new Art { Oid = 1, Artikeltext1 = "Demo text 1", Artikeltext2 = "demo text2", Artikeltext3 = "demo text 3", ArtNr = 1001, Bestand = 0, Bezeichnung = "descriptions", Stueckzahl = 10, StueckzahlLieferbar = 1, Verpackungseinheit = 1 });
            testProducts.Add(new Art { Oid = 2, Artikeltext1 = "Demo text 1", Artikeltext2 = "demo text2", Artikeltext3 = "demo text 3", ArtNr = 1001, Bestand = 0, Bezeichnung = "descriptions", Stueckzahl = 10, StueckzahlLieferbar = 1, Verpackungseinheit = 1 });
            testProducts.Add(new Art { Oid = 3, Artikeltext1 = "Demo text 1", Artikeltext2 = "demo text2", Artikeltext3 = "demo text 3", ArtNr = 1001, Bestand = 0, Bezeichnung = "descriptions", Stueckzahl = 10, StueckzahlLieferbar = 1, Verpackungseinheit = 1 });
            testProducts.Add(new Art { Oid = 4, Artikeltext1 = "Demo text 1", Artikeltext2 = "demo text2", Artikeltext3 = "demo text 3", ArtNr = 1001, Bestand = 0, Bezeichnung = "descriptions", Stueckzahl = 10, StueckzahlLieferbar = 1, Verpackungseinheit = 1 });

            return testProducts;
        }
    }
}
