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
        private Token _token;

        public Token Token
        {
            get => _token;
            set
            {
                Set(ref _token, value);
                RaisePropertyChanged(nameof(CanSearch));
            }
        }

        public IQueryable Query { get; set; }
        public SearchType SearchType { get; set; }
        public bool CanSearch
        {
            get
            {
                return Token != null;
            }
        }

        public List<Tweet> SearchResult { get; }

        public TweetSearcher()
        {
            SearchResult = new List<Tweet>();
        }

        public TweetSearcher(Token token)
        {
            Token = token;
            SearchResult = new List<Tweet>();
        }

        public void Search()
        {
            if (!CanSearch)
            {
                return;
            }

            SearchResult.Clear();

            var scrapper = new Scrapper(Token, Query);
            SearchResult.AddRange(scrapper.Scrap());
        }
    }
}
