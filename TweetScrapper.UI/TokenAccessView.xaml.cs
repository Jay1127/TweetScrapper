using System.Windows;
using TweetScrapper.UI.ViewModel;

namespace TweetScrapper.UI
{
    /// <summary>
    /// TokenAccessView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TokenAccessView : Window
    {
        public TokenAccessView()
        {
            InitializeComponent();

            (this.DataContext as IRequestCloseViewModel).RequestClose += (sender, e) =>
            {
                this.Close();
            };
        }
    }
}
