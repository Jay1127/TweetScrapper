using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Tweet를 쿼리를 통해 가져옴.
    /// </summary>
    public class TweetScrapper : IScrapper<TweetSearchQuery, Tweet>
    {
        /// <summary>
        /// 인증 토큰
        /// </summary>
        private Token _token;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="token">인증 토큰</param>
        public TweetScrapper(Token token)
        {
            _token = token;
        }

        /// <summary>
        /// Tweet을 스크랩함.
        /// </summary>
        /// <param name="scrapQueryInfo">스크랩할 쿼리문</param>
        /// <returns>Tweets</returns>
        public IEnumerable<Tweet> Scrap(TweetSearchQuery scrapQueryInfo)
        {
            var tweets = new List<Tweet>();

            // 최대 갯수가 아닌 UntilDate로 모두 가져오는 걸로 변경...!

            // 최대 갯수 (MaxScrapTweetCount - 1) + TweetCountPerPage
            while (tweets.Count < scrapQueryInfo.MaxTweetCount)
            {
                var tweetsPerPage = ScrapPerPage(scrapQueryInfo);
                tweets.AddRange(tweetsPerPage);

                scrapQueryInfo.MaxId = tweetsPerPage.Min(tweet => tweet.Id) - 1;
            }

            return tweets;
        }

        /// <summary>
        /// Tweet을 한페이지만 가져옴.
        /// </summary>
        /// <param name="queryInfo">해당 쿼리문</param>
        /// <returns>한페이지의 Tweet</returns>
        public IEnumerable<Tweet> ScrapPerPage(TweetSearchQuery queryInfo)
        {
            return ParseJsonToTweet(ReceviceTweetData(queryInfo));
        }

        /// <summary>
        /// 트위터 API에서 json데이터를 받음.
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        private string ReceviceTweetData(TweetSearchQuery queryInfo)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(queryInfo.BuildQueryUrl());
            request.Headers.Add("Authorization", $"{_token.TokenType} {_token.AccessToken}");
            request.Method = "Get";

            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// tweet api에서 받은 데이터를 파싱함.
        /// </summary>
        /// <param name="jsonString">트위터 api에서 받은 json 데이터</param>
        /// <returns>tweet</returns>
        private IEnumerable<Tweet> ParseJsonToTweet(string jsonString)
        {
            JObject o = JObject.Parse(jsonString);
            return o["statuses"].Select(jToken => new Tweet(jToken["id"].Value<ulong>(), 
                                                            jToken["text"].Value<string>(),
                                                            ParseTweetDateToDateTime(jToken["created_at"].Value<string>())));

        }

        /// <summary>
        /// Tweet에서 받은 DateTime문자열을 .Net의 DateTime으로 파싱함.
        /// </summary>
        /// <param name="dateString">Tweet에서 받은 DateTime문자열</param>
        /// <returns>DateTime으로</returns>
        private DateTime ParseTweetDateToDateTime(string dateString)
        {
            string[] monthExpression =
            {
                "Jan","Feb","Mar", "Apr","May","Jun","JuL","AuG","Sep","Oct","Nov","Dec"
            };

            var dateTokens = dateString.Split(' ');
            var monthString = dateTokens[1];
            var dayString = dateTokens[2];
            var yearString = dateTokens[5];

            int.TryParse(dayString, out int day);
            int.TryParse(yearString, out int year);
            int month = Array.IndexOf(monthExpression, monthString) + 1;

            return new DateTime(year, month, day);
        }
    }
}
