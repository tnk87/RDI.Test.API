using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDI.Test.API.Controllers;

namespace RDI.Test.API.Tests.Controllers
{
    [TestClass]
    [TestCategory("CreditCardController")]
    public class CreditCardControllerTest
    {
        [TestMethod]
        public void CheckValidCreditCard()
        {
            var ccControler = new CreditCardController();
            var result = ccControler.Add(new CreditCardInfo() { CardNumber = 4256321658974868, CVV = 25678 });


            Assert.AreEqual(typeof(JsonResult), result.GetType());
        }

        [TestMethod]
        public void CheckInvalidCreditCard()
        {
            var ccControler = new CreditCardController();
            var result = ccControler.Add(new CreditCardInfo() { CardNumber = 42563216589748689, CVV = 25678 });


            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }

        [TestMethod]
        public void CheckInvalidCVV()
        {
            var ccControler = new CreditCardController();
            var result = ccControler.Add(new CreditCardInfo() { CardNumber = 4256321658974868, CVV = 256789 });


            Assert.AreEqual(typeof(BadRequestObjectResult), result.GetType());
        }
    }
}
