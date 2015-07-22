using System;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.HttpRequest;

namespace SitecoreSignalR.Tools.Configuration
{
    public class SignalRIgnoreUrlPrefix : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            string[] prefixes = { "/signalr/" };
            if (prefixes.Length > 0)
            {
                string filePath = args.Url.FilePath;
                for (int i = 0; i < prefixes.Length; i++)
                {
                    if (filePath.StartsWith(prefixes[i], StringComparison.OrdinalIgnoreCase))
                    {
                        args.AbortPipeline();
                        return;
                    }
                }
            }
        }
    }
}