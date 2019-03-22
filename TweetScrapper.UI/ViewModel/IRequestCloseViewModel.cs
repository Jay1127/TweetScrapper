using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI.ViewModel
{
    public interface IRequestCloseViewModel
    {
        event EventHandler RequestClose;
    }
}
