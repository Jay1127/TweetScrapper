using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    public class Scrapper : ITweetScrapper
    {
        /// <summary>
        /// 인증 토큰
        /// </summary>
        public Token Token { get; set; }
        public IQueryable Query { get; set; }

        public Scrapper(Token token, IQueryable query)
        {
            Token = token;
            Query = query;
        }

        public IEnumerable<Tweet> Scrap()
        {
            return TweetParser.Parse(ReceviceTweetData());
            //return Parse(ReceviceTweetData());
        }

        public IEnumerable<Tweet> ScrapAll()
        {
            var tweets = new List<Tweet>();

            // 최대 갯수가 아닌 UntilDate로 모두 가져오는 걸로 변경...!

            // 최대 갯수 (MaxScrapTweetCount - 1) + TweetCountPerPage
            while (tweets.Count < Query.MaxCount)
            {
                var tweetsPerPage = Scrap();
                tweets.AddRange(tweetsPerPage);

                Query.MaxId = tweetsPerPage.Min(tweet => tweet.Id) - 1;
            }

            return tweets;
        }

        /// <summary>
        /// 트위터 API에서 json데이터를 받음.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private string ReceviceTweetData()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Query.BuildQueryUrl());
            request.Headers.Add("Authorization", $"{Token.TokenType} {Token.AccessToken}");
            request.Method = "Get";

            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}
