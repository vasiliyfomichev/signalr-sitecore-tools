using System;
using Sitecore;
using Sitecore.Diagnostics;

namespace SitecoreSignalR.Tools.Models
{
    public class Cache
    {
        #region Constructor

        public Cache(Sitecore.Caching.Cache cache)
        {
            Assert.IsNotNull(cache, "cache");
            Name = cache.Name;
            Count = cache.Count;
            Size = StringUtil.GetSizeString(cache.Size);
            MaxSize = StringUtil.GetSizeString(cache.MaxSize);
            Usage = Math.Round(cache.Size / (double)cache.MaxSize, 2);
            if (Usage >= .8)
            {
                SuggestedSize = StringUtil.GetSizeString((long)(cache.Size * 1.5));
            }
        }

        #endregion

        #region Properties

        public string Name { get; private set; }
        public int Count { get; private set; }
        public string Size { get; private set; }
        public double Usage { get; private set; }
        public string MaxSize { get; private set; }
        public string SuggestedSize { get; set; }

        #endregion
    }
}