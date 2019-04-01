using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper.UI
{
    public class TweetRepository
    {
        public Token Token { get; set; }

        private List<Tweet> _tweets;

        public Tweet this[int index]
        {
            get => _tweets[index];
            set => _tweets[index] = value;
        }

        public void Add(Tweet tweet)
        {
            _tweets.Add(tweet);
        }

        public void AddAll(IEnumerable<Tweet> tweets)
        {
            foreach(var tweet in tweets)
            {
                Add(tweet);
            }
        }
        
        public void Clear()
        {
            _tweets.Clear();
        }
    }
}
