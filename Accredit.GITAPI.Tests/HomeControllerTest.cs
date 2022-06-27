using Accredit.GITAPI.Service;
using Accredit.GITAPI.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Configuration;
using System.Web.Mvc;

namespace Accredit.GITAPI.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IGITAPIService> mockIGITAPIService;

        [TestInitialize]
        public void Init()
        {
            mockIGITAPIService = new Mock<IGITAPIService>();
        }

        [TestMethod]
        public void TestDetailsRedirect()
        {
            //Arrange
            var controller = new HomeController(mockIGITAPIService.Object);

            //Act
            var result = (RedirectToRouteResult)controller.Details("");

            //Assert
            Assert.AreEqual("Search", result.RouteValues["Action"]);
        }
    }
}
