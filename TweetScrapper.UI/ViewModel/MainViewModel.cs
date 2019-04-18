using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace TweetScrapper.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly TweetSearcher _tweetSearcher;

        public IList<IScrapItem> Tweets { get; set; }

        public TokenAccessViewModel TokenAccessViewModel { get; }
        public TweetSearchViewModel TweetSearchViewModel { get; }

        public RelayCommand ShowAccessDialogCommand { get; }

        public RelayCommand SaveCommand { get; }
        public RelayCommand ExitCommand { get; }        

        public MainViewModel()
        {
            Tweets = new List<IScrapItem>();
            _tweetSearcher = new TweetSearcher();

            SaveCommand = new RelayCommand(Save);
            ExitCommand = new RelayCommand(Exit);
            ShowAccessDialogCommand = new RelayCommand(ShowAccessDialog);
            TokenAccessViewModel = new TokenAccessViewModel(_tweetSearcher);            
            TweetSearchViewModel = new TweetSearchViewModel(_tweetSearcher);

            TweetSearchViewModel.Tweets.CollectionChanged += Tweets_CollectionChanged;

            TokenAccessViewModel.TokenAccessRequested += () =>
            {
                TweetSearchViewModel.UpdateStatus();
            };
        }

        private void Tweets_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach(var tweet in e.NewItems.Cast<Tweet>())
                {
                    Tweets.Add(tweet);
                }
            }
            else if(e.Action == NotifyCollectionChangedAction.Reset)
            {
                Tweets.Clear();
            }
        }

        private void Save()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "xlsx file (*.xlsx)|*.xlsx|json (*.json)|*.json|txt file (*.txt)|*.txt";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    var exporter = CreateExporter(Path.GetExtension(dialog.FileName));
                    exporter.Export(dialog.FileName, Tweets);
                }
            }
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

            TweetSearchViewModel.UpdateStatus();
        }

        public void Exit()
        {
            System.Windows.Application.Current.Shutdown();
        }

        private IExportable CreateExporter(string extension)
        {
            IExportable exporter = null;

            if (extension == ".xlsx")
            {
                exporter = new ExcelExporter();
            }
            else if (extension == ".json")
            {
                exporter = new JsonExporter();
            }
            else if (extension == ".txt")
            {
                exporter = new TextExporter();
            }

            return exporter;
        }
    }
}