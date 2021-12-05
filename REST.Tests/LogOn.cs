using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net;
using System.Linq;
using Newtonsoft.Json;

namespace REST.Tests
{
    /// <summary>
    /// Сводное описание для LogOn
    /// </summary>
    [TestClass]
    public class LogOn
    {

        public static string baseUrl = "172.28.5.44";
        RestClient RestClient;

        private static readonly UriBuilder UrlBuilder = new UriBuilder
        {
            Scheme = Uri.UriSchemeHttps,
            Host = baseUrl
        };


        public IRestResponse Login(string username = "admin", string password = "Qwerty`123")
        {
            RestClient client = new RestClient(UrlBuilder.ToString())
            {
                RemoteCertificateValidationCallback =
                    (sender, certificate, chain, sslPolicyErrors) => true
            };

            client.ThrowOnDeserializationError = true;

            var request = new RestRequest("api/auth", Method.POST) { RequestFormat = DataFormat.Json };
            request.AddJsonBody(new { login = username, password });
            var response = client.Execute(request);
            var sessionCookie = response.Cookies.FirstOrDefault();
            var cookieJar = new CookieContainer();

            if (sessionCookie != null)
            {
                cookieJar.Add(new Cookie(sessionCookie.Name, sessionCookie.Value, sessionCookie.Path,
                    sessionCookie.Domain));
            }

            client.CookieContainer = cookieJar;
            RestClient = client;
            return response;
        }


        //public IRestResponse<T> GetResponse<T>(string source, Method method, object body = null,
        //    bool isFileBody = false, string fileFieldName = null, int timeout = 0) where T : new()
        //{
        //    var request = new RestRequest(source, method) { RequestFormat = DataFormat.Json };
        //    if (timeout != 0) request.Timeout = timeout;
        //    if (isFileBody)
        //    {
        //        request.AddFile(fileFieldName, (string)body);
        //    }
        //    else if (body != null)
        //    {
        //        request.AddJsonBody(body);
        //    }

        //    try
        //    {
        //        return RestClient.Execute<T>(request);
        //    }
        //    catch (Exception)
        //    {
        //        var nonDeserialized = RestClient.Execute(request);

        //        throw;
        //    }

        //}



        [TestMethod]
        public void REST_LogOn()
        {
            var response = Login();
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
