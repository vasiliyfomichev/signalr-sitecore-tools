using System.Collections.Generic;
using Microsoft.AspNet.SignalR;
using SitecoreSignalR.Tools.Configuration;
using SitecoreSignalR.Tools.Models.Services;

namespace SitecoreSignalR.Tools.Hubs
{
    [SitecoreToolHubAuthorize]
    public class JobHub : Hub
    {
        #region Fields

        private readonly JobService _jobService;

        #endregion

        #region Constructors

        public JobHub() : this(JobService.Instance) { }

        public JobHub(JobService jobService)
        {
            _jobService = jobService;
        }

        #endregion

        #region Methods

        public IEnumerable<Models.Job> GetJobs(bool showFinished)
        {
            _jobService.ShowFinished = showFinished;
            return _jobService.GetJobs();
        }

        public void UpdateFilter(bool showFinished)
        {
            _jobService.ShowFinished = showFinished;
        }

        #endregion
    }
}