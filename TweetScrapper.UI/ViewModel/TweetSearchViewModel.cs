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
        private AdvancedSearchViewModel _advancedOptionViewModel;
        private readonly TweetSearcher _tweetSearcher;
        private string _searchKeyword;
        private SearchType _searchType;

        public AdvancedSearchViewModel AdvancedOptionViewModel
        {
            get => _advancedOptionViewModel;
            set => Set(ref _advancedOptionViewModel, value);
        }

        public int Count
        {
            get => Tweets.Count;
        }

        public bool CanSearch
        {
            get => _tweetSearcher.CanSearch;
        }        

        /// <summary>
        /// Selected seach type(binding combobox)
        /// </summary>
        public SearchType SearchType
        {
            get => _searchType;
            set
            {
                _searchType = value;
               
                if (SearchType == SearchType.Keyword)
                {
                    _tweetSearcher.Query = new TweetSearchQuery(SearchKeyword);
                }
                else
                {
                    _tweetSearcher.Query = new UserTimelineQuery() { ScreenName = SearchKeyword };
                }
            }
        }

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
            _tweetSearcher = tweetSearcher;
            Tweets = new ObservableCollection<Tweet>();
            SearchCommand = new RelayCommand(Search);
            ShowAdvancedOptionCommand = new RelayCommand(ShowAdvancedOption);
            UpdateStatus();
        }

        public void UpdateStatus()
        {
            RaisePropertyChanged(nameof(SearchType));
            RaisePropertyChanged(nameof(CanSearch));

            if (CanSearch)
            {
                SearchKeyword = string.Empty;
            }
            else
            {
                SearchKeyword = "Need to authorize (Settings → Access)";
            }
        }

        /// <summary>
        /// search tweets action
        /// </summary>
        private void Search()
        {
            UpdateKeywordToQuery();

            Tweets.Clear();

            foreach (var tweet in _tweetSearcher.Search())
            {
                Tweets.Add(tweet);
            }

            RaisePropertyChanged(nameof(Count));            
        }

        private void ShowAdvancedOption()
        {
            if (AdvancedOptionViewModel != null)
            {
                AdvancedOptionViewModel = null;
                return;
            }

            if (SearchType == SearchType.Keyword)
            {
                AdvancedOptionViewModel = new AdvancedKeywordSearchViewModel(_tweetSearcher);
            }
            else
            {
                AdvancedOptionViewModel = new AdvancedTimelineSearchViewModel(_tweetSearcher);
            }

            AdvancedOptionViewModel.WorkCompleted += (s, e) =>
            {
                AdvancedOptionViewModel = null;
            };
        }

        private void UpdateKeywordToQuery()
        {
            if (SearchType == SearchType.Keyword)
            {
                (_tweetSearcher.Query as TweetSearchQuery).Keyword = SearchKeyword;
            }
            else
            {
                (_tweetSearcher.Query as UserTimelineQuery).ScreenName = SearchKeyword;
            }
        }
    }
}
