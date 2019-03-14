using System;
using MasspackWebApi.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MasspackWebApi.Tests.Controllers
{
    [TestClass]
    public class TestControllerTest
    {
        [TestMethod]
        public void LocalDbTest_ShouldBeTrue()
        {
            TestController controller = new TestController();
            var result = controller.LocalDbTest();

            Assert.AreEqual(result, true);
        }
    }
}
