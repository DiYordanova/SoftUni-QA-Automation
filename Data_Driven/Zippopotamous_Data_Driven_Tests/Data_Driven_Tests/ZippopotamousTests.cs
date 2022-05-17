using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System.Net;

namespace Data_Driven_Tests
{
    public class ZippopotamousTests
    {
        [TestCase("BG", "9000", "Varna")]
        [TestCase("BG", "1000", "Sofija")]
        [TestCase("BG", "7000", "Ruse")]
        [TestCase("DE", "01067", "Dresden")]
        [TestCase("GB", "B1", "Birmingham")]
        

        public void Test_Zippopotamus(string countryCode,string zipCode, string expectedPlace)
        {
            var restClient = new RestClient("http://api.zippopotam.us");
            var request = new RestRequest(countryCode + "/" + zipCode);
            var response = restClient.Execute(request);
            var location = new JsonDeserializer().Deserialize<Location>(response);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(countryCode, location.CountryAbbreviation);
            Assert.AreEqual(zipCode, location.PostCode);
            StringAssert.Contains(expectedPlace, location.Places[0].PlaceName);
        }        
    }
}