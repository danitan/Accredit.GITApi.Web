using Accredit.GITAPI.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Accredit.GITAPI.Tests
{
    [TestClass]
    public class ServiceTest
    {
        private GITAPIService _GITAPIService;

        [TestInitialize]
        public void Init()
        {
            _GITAPIService = new GITAPIService();
            ConfigurationManager.AppSettings["GetUserUrl"] = "https://api.github.com/users/{0}";
        }

        [TestMethod]
        public void GetUser_Success()
        {
            // Arrange
            var username = "robconery";

            // Act
            var response = _GITAPIService.GetUser(username).Result;

            // assert
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public async Task GetUser_Failed_NoUserFound()
        {
            // Arrange
            var username = ".";

            // assert
            await Assert.ThrowsExceptionAsync<HttpRequestException>(() => _GITAPIService.GetUser(username));
        }
    }
}
