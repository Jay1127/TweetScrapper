using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public class AdvancedTimelineSearchViewModel : AdvancedSearchViewModel
    {
        public AdvancedTimelineSearchViewModel(TweetSearcher tweetSearcher)
            : base(tweetSearcher, new UserTimelineQuery(tweetSearcher.Query as UserTimelineQuery))
        {
        }
    }
}
