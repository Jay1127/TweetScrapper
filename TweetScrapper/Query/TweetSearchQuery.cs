using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Scrap Option
    /// </summary>
    public enum TweetSearchOption
    {
        /// <summary>
        /// 혼합
        /// </summary>
        Mixed,

        /// <summary>
        /// 최신순
        /// </summary>
        Recently,

        /// <summary>
        /// 인기순
        /// </summary>
        Popular
    }

    /// <summary>
    /// 특정 검색어로 트위터를 검색하는 쿼리
    /// </summary>
    public class TweetSearchQuery : IQueryable
    {
        /// <summary>
        /// Base query url
        /// </summary>
        public string BaseUrl { get; } = "https://api.twitter.com/1.1/search/tweets.json?";

        /// <summary>
        /// 검색 키워드
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 가져올 Tweet의 언어
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Tweet 검색 옵션(Iterate할 경우 Recently이외에 옵션에 대해서 확인해야함.)
        /// </summary>
        public TweetSearchOption SearchOption { get; set; }

        /// <summary>
        /// 페이지당 가져올 Tweet의 수(최대 100)
        /// </summary>
        public int CountPerQuery
        {
            get
            {
                return MaxCount < 100 ? MaxCount : 100;
            }
        }

        /// <summary>
        /// 최대로 가져올 트위터의 수
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 해당 id보다 작거나 같은 tweet를 가져옴.
        /// </summary>
        public ulong MaxId { get; set; }

        /// <summary>
        /// 해당 날짜 이전에 생성된 Tweet을 가져옴.(최대 일주일만 가져올 수 있음.)
        /// </summary>
        public DateTime UntilDate { get; set; }

        public TweetSearchQuery()
            : this(string.Empty)
        {

        }

        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="keyword">검색 문자열</param>
        /// <param name="language">검색되는 tweet의 언어 제한</param>
        /// <param name="scrapOption">검색 옵션</param>
        /// <param name="maxTweetCount">최대 검색할 트위터의 수</param>
        public TweetSearchQuery(string keyword, 
                                string language ="ko", 
                                TweetSearchOption scrapOption = TweetSearchOption.Recently, 
                                int maxTweetCount = 15)
        {
            Keyword = keyword;
            Language = language;
            SearchOption = scrapOption;
            MaxCount = maxTweetCount;
            MaxId = ulong.MaxValue;
            UntilDate = DateTime.Now;
        }

        public TweetSearchQuery(TweetSearchQuery query)
            : this(query.Keyword, query.Language, query.SearchOption, query.MaxCount)
        {

        }


        /// <summary>
        /// 쿼리 URL를 생성
        /// </summary>
        /// <returns>쿼리 URL</returns>
        public string BuildQueryUrl()
        {
            return $"{BaseUrl}" +
                   $"q={Keyword}&" +
                   $"lang={Language}&" +
                   $"count={CountPerQuery}&" +
                   $"max_id={MaxId}";
        }
    }
}
