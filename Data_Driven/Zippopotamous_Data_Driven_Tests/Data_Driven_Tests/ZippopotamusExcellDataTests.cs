using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using SpreadsheetLight;
using System.Collections.Generic;

namespace Data_Driven_Tests
{
    class ZippopotamusExcellDataTests
    {
        [TestCaseSource("LoadTestDataFromExcel")]
        public void Test_Zippopotamus(string countryCode, string zipCode, string place, string expectedStateCode)
        {
            var restClient = new RestClient("http://api.zippopotam.us");
            var request = new RestRequest(countryCode + "/" + zipCode);
            var response = restClient.Execute(request);
            var location = new JsonDeserializer().Deserialize<Location>(response);

            StringAssert.Contains(place, location.Places[0].PlaceName);
            StringAssert.Contains(expectedStateCode, location.Places[0].StateAbbreviation);
        }

        static IEnumerable<TestCaseData> LoadTestDataFromExcel()
        {
            using (var sheet = new SLDocument("../../../ZippopotamousTestData.xlsx"))
            {
                int endRowIndex = sheet.GetWorksheetStatistics().EndRowIndex;
                for (int row = 2; row < endRowIndex; row++)
                {
                    string countryCode = sheet.GetCellValueAsString(row, 1);
                    string zipCode = sheet.GetCellValueAsString(row, 2);
                    string place = sheet.GetCellValueAsString(row,3);
                    string stateAbbreviation = sheet.GetCellValueAsString(row, 4);
                    yield return new TestCaseData(countryCode, zipCode, place, stateAbbreviation);
                }
            }
            
        }
    }
}
