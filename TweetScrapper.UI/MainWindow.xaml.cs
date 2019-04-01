using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
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

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
