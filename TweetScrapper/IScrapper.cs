using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    public interface ITweetScrapper
    {
        IEnumerable<Tweet> Scrap();

        IEnumerable<Tweet> ScrapAll();
    }
}
