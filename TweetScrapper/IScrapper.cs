using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Scrap하기 위한 인터페이스
    /// </summary>
    /// <typeparam name="T">아이템을 얻기 위한 쿼리</typeparam>
    /// <typeparam name="R">쿼리를 통해 얻어질 아이템</typeparam>
    public interface IScrapper<T,R> where T : IQueryable where R: IScrapItem
    {
        /// <summary>
        /// 쿼리를 통해 아이템을 가져옵니다.
        /// </summary>
        /// <param name="query">쿼리</param>
        /// <returns>아이템</returns>
        IEnumerable<R> Scrap(T query);
    }
}
