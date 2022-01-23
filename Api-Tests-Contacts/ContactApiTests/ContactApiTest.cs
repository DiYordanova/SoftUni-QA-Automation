using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace ContactApiTests
{
    public class ApiTestsContactBook
    {
        const string BaseURL = "https://contactbook.nakov.repl.co/api";
        RestClient client;

        [SetUp]
        public void Setup()
        {
            client = new RestClient(BaseURL);
        }

        [Test]
        public void Test_ListContact_CheckForSteveJobs()
        {
            RestRequest request = new RestRequest("/contacts", Method.GET);            
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK,response.StatusCode);
           
            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);
            Assert.That(contacts.Count > 0);
            Assert.AreEqual("Steve", contacts[0].firstName);
            Assert.AreEqual("Jobs", contacts[0].lastName);
        }

        [Test]
        public void Test_FindExistingContact()
        {
            RestRequest request = new RestRequest("/contacts/search/{keyword}", Method.GET);
            request.AddUrlSegment("keyword", "albert");
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);
            Assert.That(contacts.Count > 0);
            Assert.AreEqual("Albert", contacts[0].firstName);
            Assert.AreEqual("Einstein", contacts[0].lastName);
        }

        [Test]
        public void Test_FindNonExistingContact()
        {
            RestRequest request = new RestRequest("/contacts/search/{keyword}", Method.GET);
            request.AddUrlSegment("keyword", "keyword" + DateTime.Now.Ticks);
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);
            Assert.AreEqual(0, contacts.Count);            
        }


        [Test]
        public void Test_CreatContactInvalidDate()
        {
            RestRequest request = new RestRequest("/contacts", Method.POST);
            request.AddJsonBody(new
            {
                firstName = "Someone"
            });
            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            
        }

        [Test]
        public void Test_CreatContactValidDate()
        {
            // Add new contact
            RestRequest request = new RestRequest("/contacts", Method.POST);
            var newContact = new
            {
                firstName = "fname" + DateTime.Now.Ticks,
                lastName = "lname" + DateTime.Now.Ticks,
                email = "email" + DateTime.Now.Ticks + "@abv.bg",
                phone = "+359" + DateTime.Now.Ticks,
                comments = "Some Comment" + DateTime.Now.Ticks

            };
            request.AddJsonBody(newContact);
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            // Find the contact and assert it is correctly aadded
            RestRequest requestContacts = new RestRequest("/contacts", Method.GET);
            IRestResponse responseContacts = client.Execute(requestContacts);

            Assert.AreEqual(HttpStatusCode.OK, responseContacts.StatusCode);
            List<Contact> contacts = JsonSerializer.Deserialize<List<Contact>>(responseContacts.Content);
            var lastContact = contacts.Last();
            Assert.AreEqual(newContact.firstName, lastContact.firstName);
            Assert.AreEqual(newContact.lastName, lastContact.lastName);
            Assert.AreEqual(newContact.email, lastContact.email);
            Assert.AreEqual(newContact.phone, lastContact.phone);
            Assert.AreEqual(newContact.comments, lastContact.comments);
        }
    }
}