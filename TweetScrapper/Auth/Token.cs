using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// 인증 토큰
    /// </summary>
    public class Token
    {
        /// <summary>
        /// 토큰 타입
        /// </summary>
        public string TokenType { get; }

        /// <summary>
        /// 엑세스 토큰
        /// </summary>
        public string AccessToken { get; }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="tokenType">토큰 타입</param>
        /// <param name="accessToken">엑세스 토큰</param>
        public Token(string tokenType, string accessToken)
        {
            TokenType = tokenType;
            AccessToken = accessToken;
        }
    }
}
