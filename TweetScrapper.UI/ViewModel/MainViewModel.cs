using System;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TweetScrapper.UI.ViewModel
{
    public class MainViewModel : ViewModelBase, IRequestCloseViewModel
    {
        private ViewModelBase _advancedOptionViewModel;
        private readonly TweetSearcher _tweetSearcher;

        public TokenAccessViewModel TokenAccessViewModel { get; }
        public TweetSearchViewModel TweetSearchViewModel { get; }

        public ViewModelBase AdvancedOptionViewModel
        {
            get => _advancedOptionViewModel;
            set => Set(ref _advancedOptionViewModel, value);
        }

        public RelayCommand ShowAccessDialogCommand { get; }

        public event EventHandler RequestClose;

        public RelayCommand SaveCommand { get; }
        public RelayCommand ExitCommand { get; }        

        public MainViewModel()
        {
            _tweetSearcher = new TweetSearcher();
            ExitCommand = new RelayCommand(Exit);
            ShowAccessDialogCommand = new RelayCommand(ShowAccessDialog);
            TokenAccessViewModel = new TokenAccessViewModel(_tweetSearcher);
            TweetSearchViewModel = new TweetSearchViewModel(_tweetSearcher)
            {
                ShowAdvancedOptionCommand = new RelayCommand(ShowAdvancedOption)
            };
        }

        public void ShowAccessDialog()
        {
            var view = new TokenAccessView()
            {
                DataContext = TokenAccessViewModel,                
            };

            TokenAccessViewModel.RequestClose += (sender, e) =>
            {
                view.Close();
            };

            view.ShowDialog();
            
            if (_tweetSearcher.CanSearch)
            {
                TweetSearchViewModel.SearchKeyword = string.Empty;
            }
            else
            {
                TweetSearchViewModel.InitSearchKeyword();
            }
        }

        private void ShowAdvancedOption()
        {
            if(AdvancedOptionViewModel != null)
            {
                AdvancedOptionViewModel = null;
                return;
            }

            if(TweetSearchViewModel.SearchType == SearchType.Keyword)
            {
                AdvancedOptionViewModel = new AdvancedKeywordSearchViewModel(_tweetSearcher.Query);
            }
            else
            {
                AdvancedOptionViewModel = new AdvancedTimelineSearchViewModel(_tweetSearcher.Query);
            }
        }

        public void Exit()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void CloseView(Window window)
        {
            window.Close();
        }
    }
}