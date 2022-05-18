using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;

namespace API_Tests_GitHub
{
    public class Tests
    {
        const string GitHubApiUserName = "ENTER_YOUR_GITHUB_USERNAMDE";
        const string GitHubApiPassword = "ENTER_YOUR_GITHUB_PASSWORD";        

        [Test]
        public void Test_GitHubAPI_GetIssuesByRepo()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.GET);
            
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var issues = new JsonDeserializer().Deserialize<List<IssueResponse>>(response);
            Assert.Pass();
        }

        [Test]
        public void Test_GitHubAPI_CreateNewIssue()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubApiUserName, GitHubApiPassword);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new
            {
                title = "some title",
                body = "some body",
                label = new string[] { "bug", "importance:high", "type:UI" }
            }
            );
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            Assert.IsTrue(response.ContentType.StartsWith("application/json"));

            var issues = new JsonDeserializer().Deserialize<IssueResponse>(response);

            Assert.IsTrue(issues.id > 0);
            Assert.IsTrue(issues.number > 0);
            Assert.IsTrue(!String.IsNullOrEmpty(issues.title));
            Assert.IsTrue(!String.IsNullOrEmpty(issues.body));           
        }

        [Test]
        public void Test_GitHubAPI_CreateNewIssue_Unauthorized()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            //client.Authenticator = new HttpBasicAuthenticator(GitHubApiUserName, GitHubApiPassword);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new
            {
                title = "some title",
                body = "some body",
                label = new string[] { "bug", "importance:high", "type:UI" }
            }
            );
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void Test_GitHubAPI_CreateNewIssue_MissingTitle()
        {
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubApiUserName, GitHubApiPassword);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new
            {               
                body = "some body",
                label = new string[] { "bug", "importance:high", "type:UI" }
            }
            );
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.UnprocessableEntity, response.StatusCode);
        }

        [Test]
        public void Test_GitHubAPI_DeleateComment()
        {
            //Firts create issue comment
            var client = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/3035/comments");
            client.Timeout = 3000;
            var request = new RestRequest(Method.POST);
            client.Authenticator = new HttpBasicAuthenticator(GitHubApiUserName, GitHubApiPassword);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(new
            {                
                body = "One more comment...."              
            }
            );
            var response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComment = new JsonDeserializer().Deserialize<CommentResponse>(response);

            //Second delete issue comment
            var clientDelete = new RestClient("https://api.github.com/repos/testnakov/test-nakov-repo/issues/comments/" + newComment.Id);
            clientDelete.Timeout = 3000;
            var requestDelete = new RestRequest(Method.DELETE);
            clientDelete.Authenticator = new HttpBasicAuthenticator(GitHubApiUserName, GitHubApiPassword);
            var responseDelete = clientDelete.Execute(requestDelete);

            Assert.AreEqual(HttpStatusCode.NoContent, responseDelete.StatusCode);
        }
    }
}