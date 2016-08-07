using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZillowSearch;
using ZillowSearch.Controllers;
using ZillowSearch.ZillowPropertySerach;
using Moq;

namespace ZillowSearch.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IZillowPropertySearchGateway> _zillowPropertySearchGateway;

        [TestInitialize]
        public void Initilize()
        {
            _zillowPropertySearchGateway = new Mock<IZillowPropertySearchGateway>();
        }

        [TestMethod]
        public void HomeController_TestIndex()
        {
            // Arrange
            HomeController controller = new HomeController(_zillowPropertySearchGateway.Object);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
