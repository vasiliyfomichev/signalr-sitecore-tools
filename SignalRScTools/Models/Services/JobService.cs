using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Sitecore.Jobs;
using SitecoreSignalR.Tools.Hubs;

namespace SitecoreSignalR.Tools.Models.Services
{
    public class JobService
    {
        #region Fields

        private readonly static Lazy<JobService> _instance = new Lazy<JobService>(() => new JobService(GlobalHost.ConnectionManager.GetHubContext<JobHub>().Clients));
        private readonly Timer _timer;
        private readonly object _updateJobsLock = new object();
        private volatile bool _updatingJobs = false;
        private readonly ConcurrentDictionary<string, Sitecore.Jobs.Job> _jobs = new ConcurrentDictionary<string, Sitecore.Jobs.Job> ();

        #endregion

        #region Constructor

        private JobService(IHubConnectionContext<dynamic> clients)
        {


            Clients = clients;
            _jobs.Clear();
            var jobs = JobManager.GetJobs().OrderBy(job => job.QueueTime);
            jobs.ToList().ForEach(job=> _jobs.TryAdd(string.Empty, job));
            _timer = new Timer(UpdateJobs, null, 1000, 1000);
        }

        #endregion

        #region Properties

        public bool ShowFinished { get; set; }

        public static JobService Instance
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

        public void UpdateJobs(object state)
        {
            lock (_updateJobsLock)
            {
                if (!_updatingJobs)
                {
                    _updatingJobs = true;

                    var jobs = ShowFinished ? JobManager.GetJobs().OrderBy(job => job.QueueTime) :
                        JobManager.GetJobs().OrderBy(job => job.QueueTime).Where(job => job.IsDone == false).OrderBy(job => job.QueueTime);
                    if (jobs != null && jobs.Any())
                    {
                        var jobList = jobs.Select(j => new Job(j));
                        BroadcastJobs(jobList);
                    }
                    else
                    {
                        BroadcastJobs(null);
                    }
                    _updatingJobs = false;
                }
            }
        }

        public static string GetJobText(Sitecore.Jobs.Job job)
        {
            return string.Format("{0}\n\n{1}\n\n{2}", job.Name, job.Category, GetJobMessages(job));
        }

        public static string GetJobMessages(Sitecore.Jobs.Job job)
        {
            var sb = new StringBuilder();
            if (job.Options.ContextUser != null)
                sb.AppendLine("Context User: " + job.Options.ContextUser.Name);
            sb.AppendLine("Priority: " + job.Options.Priority.ToString());
            sb.AppendLine("Messages:");
            foreach (string s in job.Status.Messages)
                sb.AppendLine(s);
            return sb.ToString();
        }

        public static string GetJobColor(Sitecore.Jobs.Job job)
        {
            if (job.IsDone)
                return "#737373";
            return "#000";
        }

        public void BroadcastJobs(IEnumerable<Job> jobs)
        {
            Clients.All.updateJobs(jobs);
        }

        public IEnumerable<Models.Job> GetJobs()
        {
            if (ShowFinished)
            {
                return _jobs.Values.Select(j => new Job(j));
            }
            else
            {
                return _jobs.Values.Where(job => job.IsDone == false).OrderBy(job => job.QueueTime).Select(j => new Job(j));
            }
        }

        #endregion
    }
}