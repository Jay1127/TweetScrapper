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
    public class TimelineScrapper : IScrapper<UserTimelineQuery, Tweet>
    {
        /// <summary>
        /// 인증 토큰
        /// </summary>
        private Token _token;

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="token">인증 토큰</param>
        public TimelineScrapper(Token token)
        {
            _token = token;
        }

        public IEnumerable<Tweet> Scrap(UserTimelineQuery query)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(query.BuildQueryUrl());
            request.Headers.Add("Authorization", $"{_token.TokenType} {_token.AccessToken}");
            request.Method = "Get";

            string jsonString = string.Empty;

            using (WebResponse response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    jsonString = reader.ReadToEnd();
                }
            }

            return TweetParser.Parse(JArray.Parse(jsonString));
        }
    }
}
