using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    /// <summary>
    /// Tweet search's(grid's row = 1) datacontext
    /// </summary>
    public class TweetSearchViewModel : ViewModelBase
    {
        public TweetSearcher TweetSearcher { get; }

        private string _searchKeyword;
        private int _count;

        public int Count
        {
            get => _count;
            set => Set(ref _count, value);
        }

        /// <summary>
        /// Selected seach type(binding combobox)
        /// </summary>
        public SearchType SearchType { get; set; }

        /// <summary>
        /// search keyword(if type is user timeline, input screenname)
        /// </summary>
        public string SearchKeyword
        {
            get => _searchKeyword;
            set => Set(ref _searchKeyword, value);
        }

        /// <summary>
        /// searching items(datagrid binding)
        /// </summary>
        public ObservableCollection<Tweet> Tweets { get; }

        /// <summary>
        /// Search Action(binding search button)
        /// </summary>
        public RelayCommand SearchCommand { get; set; }

        /// <summary>
        /// Show Advanced search option view action(binding advanced button)
        /// </summary>
        public RelayCommand ShowAdvancedOptionCommand { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        public TweetSearchViewModel(TweetSearcher tweetSearcher)
        {
            TweetSearcher = tweetSearcher;
            Tweets = new ObservableCollection<Tweet>(TweetSearcher.SearchResult);
            InitSearchKeyword();
            SearchCommand = new RelayCommand(Search);
            ShowAdvancedOptionCommand = new RelayCommand(ShowAdvancedOption);
        }

        /// <summary>
        /// Init default search keyword
        /// </summary>
        public void InitSearchKeyword()
        {
            SearchKeyword = "Need to authorize (Settings → Access)";
        }

        /// <summary>
        /// search tweets action
        /// </summary>
        private void Search()
        {
            Tweets.Clear();

            IQueryable queryable = null;
            if (SearchType == SearchType.Keyword)
            {
                queryable = new TweetSearchQuery(SearchKeyword);
            }
            else
            {
                queryable = new UserTimelineQuery() { ScreenName = SearchKeyword };
            }

            TweetSearcher.Query = queryable;
            TweetSearcher.Search();

            foreach(var result in TweetSearcher.SearchResult)
            {
                Tweets.Add(result);
            }
            Count = Tweets.Count;
        }

        private void ShowAdvancedOption()
        {
            if (SearchType == SearchType.Keyword)
            {
            }
            else
            {
            }
        }

    }
}
