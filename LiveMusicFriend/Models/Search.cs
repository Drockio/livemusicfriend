using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using LiveMusicFriend.Helpers;

namespace LiveMusicFriend.Models
{
    public class Search
    {
        public Search()
        {
            EventList = new List<Event>();
        }

        public string artist { get; set; }
        public int? artistid { get; set; }
        public string User { get; set; }
        public string apiKey { get; set; }
        public int? zip { get; set; }
        public int? radius { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string url { get; set; }
        public List<Event> EventList { get; set; }
    }
}