using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    class TweetParser
    {
        public static IEnumerable<Tweet> Parse(JArray jArray)
        {
            var tweets = new List<Tweet>();

            string url = string.Empty;
            foreach(var token in jArray)
            {
                if (token["entities"]["urls"].Count() != 0)
                {
                    url = token["entities"]["urls"][0]["url"].Value<string>();
                }

                tweets.Add(new Tweet(token["id"].Value<ulong>(),
                                     token["text"].Value<string>(),
                                     ParseTweetDateToDateTime(token["created_at"].Value<string>()), 
                                     url,
                                     token["favorite_count"].Value<int>(),
                                     token["retweet_count"].Value<int>()));
            }

            return tweets;
        }


        /// <summary>
        /// Tweet에서 받은 DateTime문자열을 .Net의 DateTime으로 파싱함.
        /// </summary>
        /// <param name="dateString">Tweet에서 받은 DateTime문자열</param>
        /// <returns>DateTime으로</returns>
        private static DateTime ParseTweetDateToDateTime(string dateString)
        {
            string[] monthExpression =
            {
                "Jan","Feb","Mar", "Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"
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
