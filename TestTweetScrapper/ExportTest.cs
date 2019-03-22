using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using TweetScrapper;

namespace TweetScrapperTest
{
    [TestClass]
    public class ExportTest
    {
        List<IScrapItem> _tweets;

        [TestInitialize]
        public void InitTweets()
        {
            _tweets = new List<IScrapItem>()
            {
                new Tweet(1,"1111111", DateTime.Now, "",0,0),
                new Tweet(2,"2222222", DateTime.Now, "",0,0),
                new Tweet(3,"3333333", DateTime.Now, "",0,0),
                new Tweet(4,"4444444", DateTime.Now, "",0,0),
                new Tweet(5,"5555555", DateTime.Now, "",0,0),
                new Tweet(6,"6666666", DateTime.Now, "",0,0),
                new Tweet(7,"7777777", DateTime.Now, "",0,0),
                new Tweet(8,"8888888", DateTime.Now, "",0,0),
                new Tweet(9,"9999999", DateTime.Now, "",0,0),
                new Tweet(10,"10101010101010", DateTime.Now, "",0,0),
                new Tweet(11,"11111111111111", DateTime.Now, "",0,0),
            };
        }

        [TestMethod]
        public void TestExportJson()
        {
            string jsonString = JsonConvert.SerializeObject(_tweets);
            string outputPath = $@"{Environment.CurrentDirectory}\testJson.json";

            File.WriteAllText(outputPath, jsonString);

            string expectedJsonString = File.ReadAllText(outputPath);
            var expectedTweets = JsonConvert.DeserializeObject<List<Tweet>>(expectedJsonString);

            Assert.AreEqual(_tweets.Count, expectedTweets.Count);
            Assert.AreEqual(jsonString, expectedJsonString);

            for(int i = 0; i < _tweets.Count; i++)
            {
                Assert.AreEqual((_tweets[i] as Tweet).Id, expectedTweets[i].Id);
                Assert.AreEqual((_tweets[i] as Tweet).Text, expectedTweets[i].Text);
            }
        }

        [TestMethod]
        public void TestExportExcel()
        {
            string outputPath = $@"{Environment.CurrentDirectory}\testExcel.xlsx";
            string[] header = { "creationTime", "Text" };

            new ExcelExporter(header).Export(outputPath, _tweets);

            Assert.IsTrue(true);
        }
    }
}
