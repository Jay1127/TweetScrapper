using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetScrapper;

namespace TweetScrapperTest
{
    [TestClass]
    public class UserTimelineQueryTest
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
        public void TestDefaultQuery()
        {
            var queryInfo = new UserTimelineQuery()
            {
                ScreenName = "twitterapi"
            };

            var scrapper = new TweetScrapper.TimelineScrapper(_token);
            var tweets = scrapper.Scrap(queryInfo);

            // 최대 갯수 (MaxScrapTweetCount - 1) + TweetCountPerPage
            //Assert.IsTrue(tweets.Count() <= (queryInfo.MaxTweetCount - 1) + queryInfo.TweetCountPerPage);
        }
    }
}
