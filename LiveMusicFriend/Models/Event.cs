using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveMusicFriend.Models
{
    public class Event
    {
        public Event(string system)
        {
            System = system;
            ArtistList = new List<Artist>();
        }

        public Venue Venue { get; set; }
        public List<Artist> ArtistList { get; set; }
        public int? ID { get; set; }
        public string System { get; set; }
        public DateTime? EventDate { get; set; }
        public Uri EventURL { get; set; }
    }
}