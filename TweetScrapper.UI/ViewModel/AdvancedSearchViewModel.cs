using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public abstract class AdvancedSearchViewModel : ViewModelBase
    {
        public event EventHandler WorkCompleted;

        public TweetSearcher TweetSearcher { get; }
        public IQueryable Query { get; set; }
        public RelayCommand ApplyCommand { get; }
        public RelayCommand CancelCommand { get; }

        public AdvancedSearchViewModel(TweetSearcher tweetSearcher, IQueryable query)
        {
            TweetSearcher = tweetSearcher;
            Query = query;
            ApplyCommand = new RelayCommand(ApplyQuery);
        }

        private void Cancel()
        {
            WorkCompleted?.Invoke(this, EventArgs.Empty);
        }

        private void ApplyQuery()
        {
            TweetSearcher.Query = Query;
            WorkCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
