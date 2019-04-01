using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// 쿼리에 대한 인터페이스
    /// </summary>
    public interface IQueryable
    {
        string BaseUrl { get; }

        ulong MaxId { get; set; }

        int CountPerQuery { get; }

        int MaxCount { get; }

        /// <summary>
        /// 쿼리 URL을 생성합니다.
        /// </summary>
        /// <returns>쿼리 URL</returns>
        string BuildQueryUrl();
    }
}
