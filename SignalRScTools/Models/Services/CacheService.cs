using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Sitecore.Caching;
using SitecoreSignalR.Tools.Hubs;

namespace SitecoreSignalR.Tools.Models.Services
{
    public class CacheService
    {
        #region Fields

        private readonly static Lazy<CacheService> _instance = new Lazy<CacheService>(() => new CacheService(GlobalHost.ConnectionManager.GetHubContext<CacheHub>().Clients));
        private readonly Timer _timer;
        private readonly object _updateCacheLock = new object();
        private volatile bool _updatingCaches = false;
        private readonly ConcurrentDictionary<string, Sitecore.Caching.Cache> _caches = new ConcurrentDictionary<string, Sitecore.Caching.Cache>();

        #endregion

        #region Constructor

        private CacheService(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _caches.Clear();
            var caches = CacheManager.GetAllCaches();
            caches.ToList().ForEach(cache => _caches.TryAdd(string.Empty, cache));
            _timer = new Timer(UpdateCaches, null, 1000, 1000);
        }

        #endregion

        #region Properties

        public static CacheService Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        public IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void UpdateCaches(object state)
        {
            lock (_updateCacheLock)
            {
                if (!_updatingCaches)
                {
                    _updatingCaches = true;

                    var caches = CacheManager.GetAllCaches();
                    if (caches != null && caches.Any())
                    {
                        var cacheList = caches.Select(j => new Cache(j)).OrderByDescending(c=>c.Usage);
                        BroadcastCaches(cacheList);
                    }
                    else
                    {
                        BroadcastCaches(null);
                    }
                    _updatingCaches = false;
                }
            }
        }

        public void BroadcastCaches(IEnumerable<Cache> caches)
        {
            Clients.All.updateCaches(caches);
        }

        public IEnumerable<Models.Cache> GetCaches()
        {
            return _caches.Values.Select(j => new Cache(j)).OrderByDescending(c => c.Usage);
        }

        #endregion
    }
}