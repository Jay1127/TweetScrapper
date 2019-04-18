using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Scrap item을 내보내기 인터페이스
    /// </summary>
    public interface IExportable
    {
        /// <summary>
        /// scrap item을 주어진 경로에 특정 파일로 내보냅니다.
        /// </summary>
        /// <param name="path">파일 내보내기 경로</param>
        /// <param name="scrapItems">내보낼 scrap item</param>
        void Export(string path, IList<IScrapItem> scrapItems);
    }
}
