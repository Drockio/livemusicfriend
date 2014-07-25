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
        string BaseURL = "http://api.jambase.com/";
        public Search SearchResults {get; set;}

        public Jambase(Search SearchInfo){
            string band = SearchInfo.artist;

            string _baseURL = "";
            if (!string.IsNullOrEmpty(band) && !string.IsNullOrEmpty(SearchInfo.artistid.ToString()))
            {
                _baseURL = BaseURL + "artists";
            }
            else
            {
                _baseURL = BaseURL + "events";
            }

            SearchResults = SearchInfo;
            SearchResults.EventList = new List<Event>();
            StringBuilder sb = new StringBuilder(_baseURL);
            sb.AddQueryParam("api_key", ApiKey);
            sb.AddQueryParam("name", band);
            sb.AddQueryParam("artistId", SearchResults.artistid.ToString());
            sb.AddQueryParam("endDate", SearchResults.endDate);
            sb.AddQueryParam("startDate", SearchResults.startDate);
            sb.AddQueryParam("user", SearchResults.User);
            sb.AddQueryParam("zipCode", SearchResults.zip.ToString());
            sb.AddQueryParam("radius", SearchResults.radius.ToString());
            sb.AddQueryParam("page", "0");
            sb.AddQueryParam("o", "xml");

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

        public int? getArtistId()
        {
            XElement xel = null;

            xel = GetViaWebRequest(Uri);

            if (xel != null)
            {
                string node = xel.XPathSelectElement("Artists/Id").Value;
                int? result = Utility.GetInt(node);
                return result;
            }
            else
                return null;
        }

        public Search getRest()
        {

            XElement xel = null;

            xel = GetViaWebRequest(Uri);

            if (xel != null)
            {
                var eventNodes = xel.Descendants("Events");

                List<Event> myEvents = new List<Event>();

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

                this.SearchResults.EventList = myEvents;
            }
            return SearchResults;
        }
    }
}