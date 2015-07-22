using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using SitecoreSignalR.Tools.Configuration;
using SitecoreSignalR.Tools.Models.Services;

namespace SitecoreSignalR.Tools.Hubs
{
    [SitecoreToolHubAuthorize]
    public class CacheHub : Hub
    {
        #region Fields 

        private readonly CacheService _cacheService;

        #endregion

        #region Constructors

        public CacheHub() : this(CacheService.Instance) { }

        public CacheHub(CacheService jobService)
        {
            _cacheService = jobService;
        }

        #endregion

        #region Methods

        public IEnumerable<Models.Cache> GetCaches()
        {
            return _cacheService.GetCaches();
        }

        #endregion
    }
}