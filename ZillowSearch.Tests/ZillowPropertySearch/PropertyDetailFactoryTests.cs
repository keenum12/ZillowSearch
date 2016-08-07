using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ZillowSearch.Models.PropertyDetails;

namespace ZillowSearch.Tests.ZillowPropertySerach
{
    [TestClass]
    public class PropertyDetailFactoryTests
    {        
        private string _xmlExampleOneResultOneRegion = "<?xml version=\"1.0\" encoding=\"utf-8\"?><SearchResults:searchresults xsi:schemaLocation=\"http://www.zillow.com/static/xsd/SearchResults.xsd http://www.zillowstatic.com/vstatic/7b56836/static/xsd/SearchResults.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SearchResults=\"http://www.zillow.com/static/xsd/SearchResults.xsd\"><request><address>29622 Ash Dale Way</address><citystatezip>Menifee, CA</citystatezip></request><message><text>Request successfully processed</text><code>0</code></message><response><results><result><zpid>2105935660</zpid><links><homedetails>http://www.zillow.com/homedetails/29623-Ash-Dale-Way-Menifee-CA-92587/2105935660_zpid/</homedetails><mapthishome>http://www.zillow.com/homes/2105935660_zpid/</mapthishome><comparables>http://www.zillow.com/homes/comps/2105935660_zpid/</comparables></links><address><street>29623 Ash Dale Way</street><zipcode>92587</zipcode><city>Menifee</city><state>CA</state><latitude>33.690411</latitude><longitude>-117.250343</longitude></address><zestimate><amount currency=\"USD\">260651</amount><last-updated>08/04/2016</last-updated><oneWeekChange deprecated=\"true\"></oneWeekChange><valueChange duration=\"30\" currency=\"USD\">4407</valueChange><valuationRange><low currency=\"USD\">247618</low><high currency=\"USD\">273684</high></valuationRange><percentile>0</percentile></zestimate><localRealEstate><region name=\"Menifee\" id=\"46500\" type=\"city\"><zindexValue>323,200</zindexValue><links><overview>http://www.zillow.com/local-info/CA-Menifee/r_46500/</overview><forSaleByOwner>http://www.zillow.com/menifee-ca/fsbo/</forSaleByOwner><forSale>http://www.zillow.com/menifee-ca/</forSale></links></region></localRealEstate></result></results></response></SearchResults:searchresults><!-- H:002  T:45ms  S:838  R:Sat Aug 06 08:50:24 PDT 2016  B:5.0.32136-master.42dc4aa~hotfix_pre.b98e1bc -->";
        private string _xmlExampleOneResultMultipleRegion = "<?xml version=\"1.0\" encoding=\"utf-8\"?><SearchResults:searchresults xsi:schemaLocation=\"http://www.zillow.com/static/xsd/SearchResults.xsd http://www.zillowstatic.com/vstatic/7b56836/static/xsd/SearchResults.xsd\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:SearchResults=\"http://www.zillow.com/static/xsd/SearchResults.xsd\"><request>    <address>2114 Bigelow Ave</address>    <citystatezip>Seattle, WA</citystatezip>    </request>    <message>    <text>Request successfully processed</text>    <code>0</code>    </message>    <response>    <results>    <result>    <zpid>48749425</zpid>    <links>    <homedetails>    http://www.zillow.com/homedetails/2114-Bigelow-Ave-N-Seattle-WA-98109/48749425_zpid/    </homedetails>    <graphsanddata>    http://www.zillow.com/homedetails/charts/48749425_zpid,1year_chartDuration/?cbt=7522682882544325802%7E9%7EY2EzX18jtvYTCel5PgJtPY1pmDDLxGDZXzsfRy49lJvCnZ4bh7Fi9w**    </graphsanddata>    <mapthishome>http://www.zillow.com/homes/map/48749425_zpid/</mapthishome>    <comparables>http://www.zillow.com/homes/comps/48749425_zpid/</comparables>    </links>    <address>    <street>2114 Bigelow Ave N</street>    <zipcode>98109</zipcode>    <city>Seattle</city>    <state>WA</state>    <latitude>47.63793</latitude>    <longitude>-122.347936</longitude>    </address>    <zestimate>    <amount currency=\"USD\">1219500</amount>    <last-updated>11/03/2009</last-updated>    <oneWeekChange deprecated=\"true\"/>    <valueChange duration=\"30\" currency=\"USD\">-41500</valueChange>    <valuationRange>    <low currency=\"USD\">1024380</low>    <high currency=\"USD\">1378035</high>    </valuationRange>    <percentile>0</percentile>    </zestimate>    <localRealEstate>    <region id=\"271856\" type=\"neighborhood\" name=\"East Queen Anne\">    <zindexValue>525,397</zindexValue>    <zindexOneYearChange>-0.144</zindexOneYearChange>    <links>    <overview>    http://www.zillow.com/local-info/WA-Seattle/East-Queen-Anne/r_271856/    </overview>    <forSaleByOwner>    http://www.zillow.com/homes/fsbo/East-Queen-Anne-Seattle-WA/    </forSaleByOwner>    <forSale>    http://www.zillow.com/east-queen-anne-seattle-wa/    </forSale>    </links>    </region>    <region id=\"16037\" type=\"city\" name=\"Seattle\">    <zindexValue>381,764</zindexValue>    <zindexOneYearChange>-0.074</zindexOneYearChange>    <links>    <overview>    http://www.zillow.com/local-info/WA-Seattle/r_16037/    </overview>    <forSaleByOwner>http://www.zillow.com/homes/fsbo/Seattle-WA/</forSaleByOwner>    <forSale>http://www.zillow.com/seattle-wa/</forSale>    </links>    </region>    <region id=\"59\" type=\"state\" name=\"Washington\">    <zindexValue>263,278</zindexValue>    <zindexOneYearChange>-0.066</zindexOneYearChange>    <links>    <overview>    http://www.zillow.com/local-info/WA-home-value/r_59/    </overview>    <forSaleByOwner>http://www.zillow.com/homes/fsbo/WA/</forSaleByOwner>    <forSale>http://www.zillow.com/wa/</forSale>    </links>    </region>    </localRealEstate>    </result>    </results>    </response></SearchResults:searchresults><!-- H:002  T:45ms  S:838  R:Sat Aug 06 08:50:24 PDT 2016  B:5.0.32136-master.42dc4aa~hotfix_pre.b98e1bc -->";

        [TestMethod]
        public void PropertyDetailsFactory_TestSingleRegionSingleResult()
        {
            // Arrange
            PropertyDetailFactory factory = new PropertyDetailFactory();
            var data = XElement.Parse(_xmlExampleOneResultOneRegion).Element("response").Element("results");

            // Act
            IList<PropertyDetail> details = factory.GetPropertyDetails(data);

            // Assert
            Assert.IsNotNull(details);
        }

        [TestMethod]
        public void PropertyDetailsFactory_TestMultipleRegionSingleResult()
        {
            // Arrange
            PropertyDetailFactory factory = new PropertyDetailFactory();
            var data = XElement.Parse(_xmlExampleOneResultMultipleRegion).Element("response").Element("results");

            // Act
            IList<PropertyDetail> details = factory.GetPropertyDetails(data);

            // Assert
            Assert.IsTrue(details.First().LocalRealEstate.Count > 1);
        }
    }
}
