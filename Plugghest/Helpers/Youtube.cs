using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace Plugghest.Helpers
{
    class Youtube
    {
        public string GetYouTubeData(string FilterBy, string videoID)
        {
            int lb = 0;
            int ub = 0;
            string videoHTML = "";
            string videoData = "";
            string vidMarker = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://gdata.youtube.com/feeds/api/videos?q=" + videoID);
            StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream());
            switch (FilterBy)
            {
                case "Title":
                    vidMarker = "<media:title type='plain'>";
                    if (string.IsNullOrEmpty(vidMarker)) return string.Empty;
                    videoHTML = sr.ReadToEnd();
                    lb = videoHTML.IndexOf(vidMarker) + vidMarker.Length;
                    ub = videoHTML.IndexOf("</media:title>", lb);
                    videoData = videoHTML.Substring(lb, ub - lb);
                    break;
                case "Views":
                    vidMarker = "viewCount='";
                    if (string.IsNullOrEmpty(vidMarker)) return string.Empty;
                    videoHTML = sr.ReadToEnd();
                    lb = videoHTML.IndexOf(vidMarker) + vidMarker.Length;
                    ub = videoHTML.IndexOf("'", lb);
                    videoData = videoHTML.Substring(lb, ub - lb);
                    break;
                case "Length":
                    vidMarker = "<yt:duration seconds='";
                    if (string.IsNullOrEmpty(vidMarker)) return string.Empty;
                    videoHTML = sr.ReadToEnd();
                    lb = videoHTML.IndexOf(vidMarker) + vidMarker.Length;
                    ub = videoHTML.IndexOf("'", lb);
                    string Seconds = videoHTML.Substring(lb, ub - lb);
                    TimeSpan t = TimeSpan.FromSeconds(int.Parse(Seconds));
                    videoData = t.Minutes + ":" + t.Seconds;
                    break;
            }
            return videoData;
        }


    }

}
