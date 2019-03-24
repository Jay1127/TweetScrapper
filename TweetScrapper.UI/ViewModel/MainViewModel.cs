using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TweetScrapper.UI.ViewModel
{
    public class MainViewModel : ViewModelBase, IRequestCloseViewModel
    {
        public TokenAccessViewModel TokenAccessViewModel { get; }
        public TweetSearchViewModel TweetSearchViewModel { get; }

        public event EventHandler RequestClose;

        public RelayCommand ShowAccessDialogCommand { get; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand ExitCommand { get; }        

        public MainViewModel()
        {
            ShowAccessDialogCommand = new RelayCommand(ShowAccessDialog);
            ExitCommand = new RelayCommand(Exit);
            TokenAccessViewModel = new ViewModelLocator().TokenAccess;
            TweetSearchViewModel = new ViewModelLocator().TweetSearch;
        }

        public void ShowAccessDialog()
        {
            new TokenAccessView().ShowDialog();

            if (TokenAccessViewModel.IsAccessed)
            {
                TweetSearchViewModel.CanSearch = true;
                TweetSearchViewModel.SearchKeyword = string.Empty;
            }
            else
            {
                TweetSearchViewModel.CanSearch = false;
                TweetSearchViewModel.InitSearchKeyword();
            }
        }

        public void Exit()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }
    }
}