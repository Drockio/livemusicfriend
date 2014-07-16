using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace LiveMusicFriend.Helpers
{
    public static class ExtensionMethods
    {

        public static StringBuilder AddQueryParam(this StringBuilder source, string key, string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            else
            {
                bool hasQuery = false;
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] == '?')
                    {
                        hasQuery = true;
                        break;
                    }
                }

                string delim;
                if (!hasQuery)
                {
                    delim = "?";
                }
                else if ((source[source.Length - 1] == '?')
                    || (source[source.Length - 1] == '&'))
                {
                    delim = string.Empty;
                }
                else
                {
                    delim = "&";
                }

                return source.Append(delim).Append(HttpUtility.UrlEncode(key))
                    .Append("=").Append(value);
            }
        }
    }
}