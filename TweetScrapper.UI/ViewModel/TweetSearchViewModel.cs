using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
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

    /// <summary>
    /// Tweet search's(grid's row = 1) datacontext
    /// </summary>
    public class TweetSearchViewModel : ViewModelBase
    {
        private string _searchKeyword;
        private bool _canSearch;

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
        /// Search Action(binding search button)
        /// </summary>
        public RelayCommand SearchCommand { get; set; }

        /// <summary>
        /// check if can search
        /// </summary>
        public bool CanSearch
        {
            get => _canSearch;
            set => Set(ref _canSearch, value);
        }

        /// <summary>
        /// constructor
        /// </summary>
        public TweetSearchViewModel()
        {
            InitSearchKeyword();
        }

        /// <summary>
        /// Init default search keyword
        /// </summary>
        public void InitSearchKeyword()
        {
            SearchKeyword = "Need to authorize (Settings → Access)";
        }
    }
}
