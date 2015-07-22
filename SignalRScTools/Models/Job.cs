using Sitecore;
using SitecoreSignalR.Tools.Models.Services;

namespace SitecoreSignalR.Tools.Models
{
    public class Job
    {
        #region Constructor

        public Job(Sitecore.Jobs.Job job)
        {
            State = job.Status.State.ToString();
            Processed = job.Status.Processed.ToString();
            Total = job.Status.Total.ToString();
            Time = job.QueueTime.ToLocalTime().ToString();
            Category = StringUtil.Clip(job.Category, 50, true);
            Name = StringUtil.Clip(job.Name, 50, true);
            Description = JobService.GetJobText(job);
            Color = JobService.GetJobColor(job);
        }

        #endregion

        #region Properties

        public string State { get; private set; }
        public string Processed { get; private set; }
        public string Total { get; private set; }
        public string Time { get; private set; }
        public string Category { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Color { get; private set; }

        #endregion
    }
}
