using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public class AdvancedKeywordSearchViewModel : ViewModelBase
    {
        public TweetSearchQuery Query { get; }

        public AdvancedKeywordSearchViewModel(IQueryable query)
        {
            Query = query as TweetSearchQuery;
        }
    }
}
