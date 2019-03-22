using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Tweet 클래스
    /// </summary>
    public class Tweet : IScrapItem
    {
        /// <summary>
        /// Tweet의 Id(유저 아이디 아님)
        /// </summary>
        public ulong Id { get; }

        public int FavoriteCount { get; }

        public int RetweetCount { get; }

        /// <summary>
        /// Tweet의 내용
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Tweet 생성시간
        /// </summary>
        public DateTime CreationTime { get; }

        /// <summary>
        /// URL(short)
        /// </summary>
        public string Url { get; }

        /// <summary>
        /// 기본 생성자
        /// </summary>
        public Tweet()
        {

        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="id">Tweet의 Id</param>
        /// <param name="text">Tweet의 내용</param>
        /// <param name="creationTime">Tweet 생성시간</param>
        public Tweet(ulong id, string text, DateTime creationTime, string url, int favoriteCount, int retweetCount)
        {
            Id = id;
            Text = text;
            CreationTime = creationTime;
            Url = url;
            FavoriteCount = favoriteCount;
            RetweetCount = retweetCount;
        }

        /// <summary>
        /// 출력(내보내기)할 문자열 을 구성함.
        /// </summary>
        /// <returns>출력할 문자열(tweet 생성시간, 내용)</returns>
        public string[] ComposePrintItmes()
        {
            return new string[] 
            {
                CreationTime.ToShortDateString(),
                Text
            };
        }

    }
}
