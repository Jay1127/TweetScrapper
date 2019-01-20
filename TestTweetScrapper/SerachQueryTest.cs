using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetScrapper;

namespace TweetScrapperTest
{
    [TestClass]
    public class SerachQueryTest
    {
        Token _token;

        [TestInitialize]
        public void Initialize()
        {
            string accessToken = "AAAAAAAAAAAAAAAAAAAAAOwsygAAAAAA5FgH%2FAFyl6R7LZaUcrbslr0Z%2B%2BU%3DcX3pxc31GLsVWHAfN70vI4SEWgQ55dw2KkVrmCMcMDq4ss13bG";
            string tokenType = "bearer";
            _token = new Token(tokenType, accessToken);
        }

        [TestMethod]
        public void TestBuildQuery()
        {
            var queryInfo = new TweetSearchQuery("나이키");
            var expectedUrl = $"https://api.twitter.com/1.1/search/tweets.json?q=나이키&lang=ko&count=15&max_id={ulong.MaxValue}";
            Assert.AreEqual(queryInfo.BuildQueryUrl(), expectedUrl);

            queryInfo = new TweetSearchQuery("아디다스", "ko", TweetSearchOption.Popular, 100);
            expectedUrl = $"https://api.twitter.com/1.1/search/tweets.json?q=아디다스&lang=ko&count=100&max_id={ulong.MaxValue}";
            Assert.AreEqual(queryInfo.BuildQueryUrl(), expectedUrl);
        }

        [TestMethod]
        public void TestDefaultQuery()
        {            
            var queryInfo = new TweetSearchQuery("나이키");

            var scrapper = new TweetScrapper.TweetScrapper(_token);
            var tweets = scrapper.Scrap(queryInfo);

            // 최대 갯수 (MaxScrapTweetCount - 1) + TweetCountPerPage
            Assert.IsTrue(tweets.Count() <= (queryInfo.MaxTweetCount - 1) + queryInfo.TweetCountPerPage);
        }

        [TestMethod]
        public void Test100Query()
        {
            TweetSearchQuery queryInfo = new TweetSearchQuery("나이키", maxTweetCount: 100);
            var scrapper = new TweetScrapper.TweetScrapper(_token);
            var tweets = scrapper.Scrap(queryInfo);

            Assert.IsTrue(tweets.Count() <= (queryInfo.MaxTweetCount - 1) + queryInfo.TweetCountPerPage);
        }
    }
}
