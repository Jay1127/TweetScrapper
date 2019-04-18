using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// 텍스트 파일 내보내기
    /// </summary>
    public class TextExporter : IExportable
    {
        public string Delimiter { get; set; }

        public TextExporter(string delimiter = ", ")
        {
            Delimiter = delimiter;
        }

        /// <summary>
        /// 텍스트 파일 내보내기
        /// </summary>
        /// <param name="path">파일 내보내기 경로</param>
        /// <param name="scrapItems">내보낼 scrap item</param>
        public void Export(string path, IList<IScrapItem> scrapItems)
        {
            var items = scrapItems.Select(item => item.ComposePrintItmes()).ToArray();
            string[] lines = new string[items.Length];

            for (int i = 0; i < items.Length - 1; i++)
            {
                var text = new StringBuilder();

                for (int j = 0; j < items[i].Length -1; j++)
                {
                    text.Append(items[i][j]);
                    text.Append(Delimiter);
                }

                text.Append(items[items.Length - 1]);

                lines[i] = text.ToString();
            }

            File.WriteAllLines(path, lines);
        }
    }
}
