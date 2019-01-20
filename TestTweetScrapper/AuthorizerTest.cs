using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using TweetScrapper;

namespace TweetScrapperTest
{
    [TestClass]
    [Ignore]
    public class AuthorizerTest
    {
        [TestMethod]
        public void TestAuthorizer()
        {
            string consumerKey = "egjNlASi3o3r4nriynMAnlTjE";
            string consumerSecret = "fhuBNzdwrs1Tt9NrkzCvYJbzQeCr5WvIGvHPOG95lod9SVJ7Qd";

            Token token = Authorizer.Authorize(consumerKey, consumerSecret);
        }
    }
}
