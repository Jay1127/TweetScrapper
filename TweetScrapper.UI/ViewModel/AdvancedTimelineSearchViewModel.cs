using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public class AdvancedTimelineSearchViewModel : ViewModelBase
    {
        public UserTimelineQuery Query { get; }

        public AdvancedTimelineSearchViewModel(IQueryable query)
        {
            Query = query as UserTimelineQuery;
        }
    }
}
