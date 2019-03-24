using System.Windows;
using TweetScrapper.UI.ViewModel;

namespace TweetScrapper.UI
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (this.DataContext as IRequestCloseViewModel).RequestClose += (sender, e) =>
            {
                this.Close();
            };
        }
    }
}
