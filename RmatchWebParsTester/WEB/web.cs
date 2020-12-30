using System;
using System.Net;
using System.Web;

namespace RmatchWebParsTester
{
    public class web
    {
       public static string Download(string URL)
        {
            WebClient _webClient = new WebClient();
            string downloadString = default;
            
            try
            {
                downloadString = _webClient.DownloadString(URL);
            }
            catch (Exception e)
            {
                downloadString = "error";
            }
     

            return downloadString;
        }

    }
}