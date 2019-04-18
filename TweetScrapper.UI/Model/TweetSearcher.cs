using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI
{
    /// <summary>
    /// search type
    /// </summary>
    public enum SearchType
    {
        /// <summary>
        /// search tweet containing keyword
        /// </summary>
        Keyword,
        /// <summary>
        /// search tweet by specific user
        /// </summary>
        UserTimeline
    }

    public class TweetSearcher : ObservableObject
    {        
        public Token Token { get; set; }
        public IQueryable Query { get; set; }

        public bool CanSearch
        {
            get => Token != null;
        }

        public TweetSearcher()
        {
            Query = new TweetSearchQuery();
        }

        public IEnumerable<Tweet> Search()
        {
            if (!CanSearch)
            {
                throw new ArgumentNullException("Need to oauthorize token");
            }

            var scrapper = new Scrapper(Token, Query);
            return scrapper.ScrapAll();
        }
    }
}
