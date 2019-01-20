using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    /// <summary>
    /// Json파일 내보내기
    /// </summary>
    public class JsonExporter : IExportable
    {
        /// <summary>
        /// Json파일 내보내기
        /// </summary>
        /// <param name="path">파일 내보내기 경로</param>
        /// <param name="scrapItems">내보낼 scrap item</param>
        public void Export(string path, List<IScrapItem> scrapItems)
        {
            if (!Directory.Exists(Path.GetPathRoot(path)))
            {
                throw new DirectoryNotFoundException();
            }

            string jsonString = JsonConvert.SerializeObject(scrapItems, Formatting.Indented);
            File.WriteAllText(path, jsonString);
        }
    }
}
