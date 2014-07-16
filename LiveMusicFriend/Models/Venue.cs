using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveMusicFriend.Models
{
    public class Venue
    {
        public int? ID {get; set;}
        public string Name {get; set;}
        public string City {get; set;}
        public string State {get; set;}
        public int? Zip {get; set;}
    }
}