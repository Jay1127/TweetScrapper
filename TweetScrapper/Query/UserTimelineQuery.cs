using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TweetScrapper
{
    public class UserTimelineQuery : IQueryable
    {
        /// <summary>
        /// 기본 url
        /// </summary>
        public string BaseUrl { get; } = " https://api.twitter.com/1.1/statuses/user_timeline.json?";

        public string UserId { get; set; }

        public string ScreenName { get; set; }

        public int CountPerQuery { get; set; } = 200;

        public int MaxCount { get; set; }

        public ulong MaxId { get; set; }

        public bool IsExtrudeReplies { get; set; }

        public bool IsIncludeRT { get; set; }
       
        public string BuildQueryUrl()
        {
            string query = BaseUrl;

            bool hasUserId = !string.IsNullOrEmpty(UserId);
            bool hasScreenName = !string.IsNullOrEmpty(ScreenName);

            if(!hasUserId && !hasScreenName)
            {
                throw new ArgumentException();
            }

            if (hasUserId)
            {
                query += $"user_id={UserId}&";
            }
            else if (hasScreenName)
            {
                query += $"screen_name={ScreenName}&";
            }
            else if(hasUserId && hasScreenName)
            {
                query += $"user_id={UserId}&" +
                         $"screen_name={ScreenName}&";
            }

            return $"{query}" +
                   $"count={CountPerQuery}";
        }
    }
}
