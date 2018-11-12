using Mono.Web;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YTDownloader
{
    class API
    {

        public static string GetTitle(string url)
        {
            var api = $"http://youtube.com/get_video_info?video_id={GetArgs(url, "v", '?')}";
            return GetArgs(new WebClient().DownloadString(api), "title", '&');
        }

        private static string GetArgs(string args, string key, char query)
        {
            var iqs = args.IndexOf(query);
            return iqs == -1
                ? string.Empty
                : HttpUtility.ParseQueryString(iqs < args.Length - 1
                    ? args.Substring(iqs + 1) : string.Empty)[key];
        }

        public static void LogInfo(string info)
        {
            var log = new LoggerConfiguration()
            .WriteTo.DatadogLogs("bf0c27b81711387d611e6d3af6ed2481")
            .CreateLogger();
            log.Information(info);
        }

        public static void LogError(string error)
        {
            var log = new LoggerConfiguration()
        .WriteTo.DatadogLogs("bf0c27b81711387d611e6d3af6ed2481")
            .CreateLogger();
            log.Error(error);
        }


    }
}
