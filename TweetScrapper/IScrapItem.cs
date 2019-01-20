using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// 스크랩된 아이템 인터페이스
    /// </summary>
    public interface IScrapItem
    {
        /// <summary>
        /// 출력(내보내기)할 때 표시될 문자열들을 구성함.
        /// </summary>
        /// <returns>출력할 때 표시될 문자열</returns>
        string[] ComposePrintItmes();

        /// <summary>
        /// 본문 내용
        /// </summary>
        string Text { get; }

        /// <summary>
        /// 생성 시간(스크랩 시간이 아닌 아이템이 생성된 시간)
        /// </summary>
        DateTime CreationTime { get; }
    }
}
