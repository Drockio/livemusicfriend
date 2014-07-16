using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveMusicFriend.Helpers
{
    public static class Utility
    {
        public static DateTime? GetDate(string DateString)
        {
            DateTime dt;
            if (DateTime.TryParse(DateString, out dt))
                return dt;
            else
                return null;
        }

        public static int? GetInt(string IntString)
        {
            int outInt;
            if (Int32.TryParse(IntString, out outInt))
                return outInt;
            else
                return null;
        }

        public static Uri GetUri(String uri)
        {
            if (Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                return new Uri(uri);
            else
                return null;
        }
    }
}