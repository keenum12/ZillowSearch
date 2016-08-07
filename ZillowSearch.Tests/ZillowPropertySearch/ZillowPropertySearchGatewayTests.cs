using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZillowSearch.ZillowPropertySerach;
using Moq;
using System.Xml.Linq;
using ZillowSearch.Models.PropertyDetails;
using System.Collections.Generic;

namespace ZillowSearch.Tests.ZillowPropertySerach
{
    [TestClass]
    public class GetSearchResultsWrapperTests
    {
        private Mock<IPropertyDetailFactory> _propertyFactoryMock;
        private Mock<IGenericRestServiceCaller> _genericRestCallerMock;
        private string _xmlExampleOneResultOneRegion = "<?xml version=\"1.0\" encoding=\"utf-8\"?><SearchResults:searchresults xsi:schemaLocation=\"http://www.zillow.com/static/xsd/SearchResults.xsd http://www.zillowstatic.com/vstatic/7b56836/static/xsd/SearchResults.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SearchResults=\"http://www.zillow.com/static/xsd/SearchResults.xsd\"><request><address>29622 Ash Dale Way</address><citystatezip>Menifee, CA</citystatezip></request><message><text>Request successfully processed</text><code>0</code></message><response><results><result><zpid>2105935660</zpid><links><homedetails>http://www.zillow.com/homedetails/29623-Ash-Dale-Way-Menifee-CA-92587/2105935660_zpid/</homedetails><mapthishome>http://www.zillow.com/homes/2105935660_zpid/</mapthishome><comparables>http://www.zillow.com/homes/comps/2105935660_zpid/</comparables></links><address><street>29623 Ash Dale Way</street><zipcode>92587</zipcode><city>Menifee</city><state>CA</state><latitude>33.690411</latitude><longitude>-117.250343</longitude></address><zestimate><amount currency=\"USD\">260651</amount><last-updated>08/04/2016</last-updated><oneWeekChange deprecated=\"true\"></oneWeekChange><valueChange duration=\"30\" currency=\"USD\">4407</valueChange><valuationRange><low currency=\"USD\">247618</low><high currency=\"USD\">273684</high></valuationRange><percentile>0</percentile></zestimate><localRealEstate><region name=\"Menifee\" id=\"46500\" type=\"city\"><zindexValue>323,200</zindexValue><links><overview>http://www.zillow.com/local-info/CA-Menifee/r_46500/</overview><forSaleByOwner>http://www.zillow.com/menifee-ca/fsbo/</forSaleByOwner><forSale>http://www.zillow.com/menifee-ca/</forSale></links></region></localRealEstate></result></results></response></SearchResults:searchresults><!-- H:002  T:45ms  S:838  R:Sat Aug 06 08:50:24 PDT 2016  B:5.0.32136-master.42dc4aa~hotfix_pre.b98e1bc -->";

        [TestInitialize]
        public void Initilize()
        {
            _propertyFactoryMock = new Mock<IPropertyDetailFactory>();
            _genericRestCallerMock = new Mock<IGenericRestServiceCaller>();

            _genericRestCallerMock.Setup(x => x.GetStringResponse(It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<IDictionary<string, string>>())).Returns(_xmlExampleOneResultOneRegion);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZillowPropertySearchGateway_InValidAddress()
        {
            // Arrange
            ZillowPropertySearchGateway service = new ZillowPropertySearchGateway(_propertyFactoryMock.Object, 
                _genericRestCallerMock.Object);

            // Act
            var t = service.GetPropertyDetails("", "Menifee, CA", false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZillowPropertySearchGateway_InValidCityState()
        {
            // Arrange
            ZillowPropertySearchGateway service = new ZillowPropertySearchGateway(_propertyFactoryMock.Object,
                _genericRestCallerMock.Object);

            // Act
            var t = service.GetPropertyDetails("29622 Ash Dale Way", "", false);
        }

        [TestMethod]
        public void ZillowPropertySearchGateway_SuccessCall()
        {
            // Arrange
            ZillowPropertySearchGateway service = new ZillowPropertySearchGateway(_propertyFactoryMock.Object,
                _genericRestCallerMock.Object);

            // Act
            var t = service.GetPropertyDetails("29622 Ash Dale Way", "95584", false);

            // Verify
            Assert.IsTrue(t.IsSuccessful);
        }
    }
}
