using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public class AdvancedKeywordSearchViewModel : AdvancedSearchViewModel
    {
        public AdvancedKeywordSearchViewModel(TweetSearcher tweetSearcher)
            : base(tweetSearcher, new TweetSearchQuery(tweetSearcher.Query as TweetSearchQuery))
        {
        }
    }
}
