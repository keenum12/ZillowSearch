using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZillowSearch.ZillowPropertySerach;
using Moq;
using System.Xml.Linq;

namespace ZillowSearch.Tests.ZillowPropertySerach
{
    [TestClass]
    public class GetSearchResultsWrapperTests
    {
        private Mock<IPropertyDetailsFactory> _propertyFactoryMock;
        private Mock<IGenericRestServiceCaller> _genericRestCallerMock;

        [TestInitialize]
        public void Initilize()
        {
            _propertyFactoryMock = new Mock<IPropertyDetailsFactory>();
            _genericRestCallerMock = new Mock<IGenericRestServiceCaller>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSearchResultsWrapper_InValidAddress()
        {
            // Arrange
            ZillowPropertySearchGateway service = new ZillowPropertySearchGateway(_propertyFactoryMock.Object, 
                _genericRestCallerMock.Object);

            // Act
            var t = service.GetPropertyDetails("", "Menifee, CA");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetSearchResultsWrapper_InValidCityState()
        {
            // Arrange
            ZillowPropertySearchGateway service = new ZillowPropertySearchGateway(_propertyFactoryMock.Object,
                _genericRestCallerMock.Object);
            var d = XDocument.Parse(null);
            // Act
            var t = service.GetPropertyDetails("29622 Ash Dale Way", "");
        }
    }
}
