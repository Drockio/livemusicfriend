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
        static public string ApiKey = ConfigurationManager.AppSettings["API_KEY"];
        const string System = "JamBase";
        static string BaseURL = "http://api.jambase.com/";
        public Search SearchResults {get; set;}

        public static List<Event> ParseEventResults(XElement searchResults)
        {
            //parse results
            List<Event> myEvents = new List<Event>();

            if (searchResults != null)
            {
                var eventNodes = searchResults.Descendants("Events");
                
                foreach (XElement xEvent in eventNodes)
                {
                    Event myEvent = new Event(System);
                    myEvent.ID = Utility.GetInt(xEvent.XPathSelectElement("Id").Value);
                    myEvent.EventDate = Utility.GetDate(xEvent.XPathSelectElement("Date").Value);
                    myEvent.EventURL = Utility.GetUri(xEvent.XPathSelectElement("TicketUrl").Value);

                    foreach (XElement xArtist in xEvent.XPathSelectElements("Artists/Artist"))
                    {
                        Artist artist = new Artist();
                        artist.ArtistID = Utility.GetInt(xArtist.XPathSelectElement("Id").Value);
                        artist.ArtistName = xArtist.XPathSelectElement("Name").Value;
                        myEvent.ArtistList.Add(artist);
                    }

                    foreach (XElement xVenue in xEvent.XPathSelectElements("Venue"))
                    {
                        Venue venue = new Venue();
                        venue.ID = Utility.GetInt(xVenue.XPathSelectElement("Id").Value);
                        venue.Name = xVenue.XPathSelectElement("Name").Value;
                        venue.City = xVenue.XPathSelectElement("City").Value;
                        venue.State = xVenue.XPathSelectElement("State").Value;
                        venue.Zip = Utility.GetInt(xVenue.XPathSelectElement("ZipCode").Value);
                        myEvent.Venue = venue;
                    }

                    myEvents.Add(myEvent);
                }
            }
            return myEvents;
        }

        public static List<Event> getPerformancesByZipCode(Search searchInfo)
        {
            //generate url
            //http://api.jambase.com/events?zipCode=95128&radius=50&startDate=2014-07-24&endDate=2014-08-24&page=0&api_key=8BDB5EXNPXANZYMMKQYXE3Y8
            var url = new StringBuilder(BaseURL + "events");
            url.AddQueryParam("endDate", searchInfo.endDate);
            url.AddQueryParam("startDate", searchInfo.startDate);
            url.AddQueryParam("zipCode", searchInfo.zip.ToString());
            url.AddQueryParam("radius", searchInfo.radius.ToString());
            url.AddQueryParam("page", "0");
            url.AddQueryParam("o", "xml");
            url.AddQueryParam("api_key", ApiKey);

            //make web request
            var searchResults = GetXMLFromWebRequest(new Uri(url.ToString()));

            return ParseEventResults(searchResults);
        }

        public static List<Event> GetPerformancesByArtist(Search searchInfo)
        {
            //get artistId
            int? artistId = GetArtistID(searchInfo.artist);

            var url = new StringBuilder(BaseURL + "events");
            url.AddQueryParam("endDate", searchInfo.endDate);
            url.AddQueryParam("startDate", searchInfo.startDate);
            url.AddQueryParam("zipCode", searchInfo.zip.ToString());
            url.AddQueryParam("radius", searchInfo.radius.ToString());
            url.AddQueryParam("page", "0");
            url.AddQueryParam("o", "xml");
            url.AddQueryParam("api_key", ApiKey);
            url.AddQueryParam("artistId", artistId.ToString());

            //make web request for performances
            var searchResults = GetXMLFromWebRequest(new Uri(url.ToString()));
            return ParseEventResults(searchResults);
        }

        private static int? GetArtistID(string artistName)
        {
            //http://api.jambase.com/artists?name=Horse+Feathers&page=0&api_key=8BDB5EXNPXANZYMMKQYXE3Y8
            var url = new StringBuilder(BaseURL + "artists");
            url.AddQueryParam("page", "0");
            url.AddQueryParam("o", "xml");
            url.AddQueryParam("api_key", ApiKey);
            url.AddQueryParam("name", artistName);

            var artistResults = GetXMLFromWebRequest(new Uri(url.ToString()));

            if (artistResults != null)
            {
                int? artistId = Utility.GetInt(artistResults.XPathSelectElement("Artists/Id").Value);
                return artistId;
            }
            else
            {
                return null;
            }
        }

        private static XElement GetXMLFromWebRequest(Uri Uri)
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
                return null;
            }
            return xel;
        }
    }
}