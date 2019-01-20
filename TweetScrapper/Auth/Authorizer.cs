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
    /// OAuth 인증 토큰 생성 클래스
    /// </summary>
    public class Authorizer
    {
        /// <summary>
        /// 생성자
        /// </summary>
        private Authorizer()
        {

        }

        /// <summary>
        /// OAuth 인증 토큰을 생성함.
        /// </summary>
        /// <param name="consumerKey">트위터 consumer key</param>
        /// <param name="consumerSecret">트위터 consumer secret</param>
        /// <returns>인증 토큰</returns>
        public static Token Authorize(string consumerKey, string consumerSecret)
        {
            return ParseJsonToToken(ReceiveAuthData(consumerKey, consumerSecret));
        }

        /// <summary>
        /// OAuth token을 얻기 위한 WebRequest를 생성함.
        /// </summary>
        /// <param name="consumerKey">트위터 cnosumer key</param>
        /// <param name="consumerSecret">트위터 consumer secret</param>
        /// <returns>HttpWebRequest</returns>
        private static HttpWebRequest CreateAuthRequest(string consumerKey, string consumerSecret)
        {
            string authUrl = "https://api.twitter.com/oauth2/token";
            string authHeaderFormat = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{Uri.EscapeDataString(consumerKey)}:{Uri.EscapeDataString(consumerSecret)}"));

            string authHeader = $"Basic {authHeaderFormat}";
            string postBody = "grant_type=client_credentials";

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(authUrl);
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Headers.Add("Accept-Encoding", "gzip");
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            return authRequest;
        }

        /// <summary>
        /// Auth 인증 데이터를 받음.
        /// </summary>
        /// <param name="consumerKey">트위터 consumer key</param>
        /// <param name="consumerSecret">트위터 consumer secret</param>
        /// <returns></returns>
        private static string ReceiveAuthData(string consumerKey, string consumerSecret)
        {
            using (var authResponse = CreateAuthRequest(consumerKey, consumerSecret).GetResponse())
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Auth 인증 데이터(json)를 파싱함.
        /// </summary>
        /// <param name="jsonString">Auth 인증 데이터(json)</param>
        /// <returns>인증 토큰</returns>
        private static Token ParseJsonToToken(string jsonString)
        {
            var o = JObject.Parse(jsonString);

            return new Token(o["token_type"].Value<string>(),
                             o["access_token"].Value<string>());
        }
    }
}
