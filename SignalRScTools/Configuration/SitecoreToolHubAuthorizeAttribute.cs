using System;
using Microsoft.AspNet.SignalR.Hubs;
using Sitecore;

namespace SitecoreSignalR.Tools.Configuration
{
    public class SitecoreToolHubAuthorizeAttribute : Attribute, IAuthorizeHubConnection
    {
        public virtual bool AuthorizeHubConnection(HubDescriptor hubDescriptor, Microsoft.AspNet.SignalR.IRequest request)
        {
            if (Context.User.IsAdministrator)
                return true;
            return false;
        }
    }
}