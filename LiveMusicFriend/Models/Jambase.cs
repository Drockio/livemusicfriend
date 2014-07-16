using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Net;
using System.IO;

using LiveMusicFriend.Helpers;
using LiveMusicFriend.Models;
using System.Text;
using System.Configuration;

namespace LiveMusicFriend.Models
{
    public class Jambase
    {
        public Uri Uri { get; set; }
        public string ApiKey = ConfigurationManager.AppSettings["API_KEY"];
        const string System = "JamBase";
        const string BaseURL = "http://api.jambase.com/search";
        public Search SearchResults {get; set;}

        public Jambase(Search SearchInfo){

            SearchResults = SearchInfo;
            SearchResults.EventList = new List<Event>();
            StringBuilder sb = new StringBuilder(BaseURL);
            sb.AddQueryParam("apikey", ApiKey);
            sb.AddQueryParam("band", SearchResults.band);
            sb.AddQueryParam("bandid", SearchResults.bandid.ToString());
            sb.AddQueryParam("endDate", SearchResults.endDate);
            sb.AddQueryParam("startDate", SearchResults.startDate);
            sb.AddQueryParam("user", SearchResults.User);
            sb.AddQueryParam("zip", SearchResults.zip.ToString());
            sb.AddQueryParam("radius", SearchResults.radius.ToString());

            Uri = new Uri(sb.ToString());
        }

        public XElement GetViaWebRequest(Uri Uri)
        {
            XElement xel = null;
            WebRequest request = WebRequest.Create(Uri);
            request.Method = "GET";
            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                {
                    xel = XElement.Load(stream);
                }
            }
            catch (Exception e)
            {

            }
            return xel;
        }

        public Search getRest()
        {

            XElement xel = null;

            xel = GetViaWebRequest(Uri);

            if (xel != null)
            {
                var eventNodes = xel.Descendants("event");

                List<Event> myEvents = new List<Event>();

                foreach (XElement xEvent in eventNodes)
                {
                    Event myEvent = new Event(System);
                    myEvent.ID = Utility.GetInt(xEvent.XPathSelectElement("event_id").Value);
                    myEvent.EventDate = Utility.GetDate(xEvent.XPathSelectElement("event_date").Value);
                    myEvent.EventURL = Utility.GetUri(xEvent.XPathSelectElement("event_url").Value);

                    foreach (XElement xArtist in xEvent.XPathSelectElements("artists/artist"))
                    {
                        Artist artist = new Artist();
                        artist.ArtistID = Utility.GetInt(xArtist.XPathSelectElement("artist_id").Value);
                        artist.ArtistName = xArtist.XPathSelectElement("artist_name").Value;
                        myEvent.ArtistList.Add(artist);
                    }

                    foreach (XElement xVenue in xEvent.XPathSelectElements("venue"))
                    {
                        Venue venue = new Venue();
                        venue.ID = Utility.GetInt(xVenue.XPathSelectElement("venue_id").Value);
                        venue.Name = xVenue.XPathSelectElement("venue_name").Value;
                        venue.City = xVenue.XPathSelectElement("venue_city").Value;
                        venue.State = xVenue.XPathSelectElement("venue_state").Value;
                        venue.Zip = Utility.GetInt(xVenue.XPathSelectElement("venue_zip").Value);
                        myEvent.Venue = venue;
                    }

                    myEvents.Add(myEvent);
                }

                this.SearchResults.EventList = myEvents;
            }
            return SearchResults;
        }
    }
}