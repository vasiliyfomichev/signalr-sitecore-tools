What are SignalR Sitecore Tools?
===================================

Sitecore provides a set of very useful tools in \Website\sitecore\admin folder. This project upgrades a set of these tools by providing live updates using SignalR technology and a more friendly SPEAK-like styled user interface. 

![Sitecore SignalR Tools](http://www.cmsbestpractices.com/wp-content/uploads/2015/07/sitecore-signalr-tools-logo.png)

The following tools are included in the package:

- cache.html - a cache.aspx and a [Cache Tuner Module](https://marketplace.sitecore.net/en/Modules/Cache_Tuner.aspx) combined in one. 
- jobs.html - SignalR implementation of jobs.aspx displaying a list of currently running and finished jobs 

All SignalR tools provide live updates every second eliminating the need for clicking the refresh button to see updates. The SignalR tools, just like their original implementations, require administrator-level access.

As the project grows, more tools will be added.


How to Deploy Sitecore SignalR Tools?
-----------------------------------------
Publish the project directly into the \Website folder and copy the SitecoreSignalRTools.config into \Website\App_Config\Include.


How to Access Sitecore SignalR Tools
---------------------------------------
All tools get published into \Website\sitecore\admin folder and can be accessed by navigating to -
- /Website/sitecore/admin/cache.html
- /Website/sitecore/admin/jobs.html


Contributing
----------------------
If you have have a contribution for this repository, please send a pull request.


License
------------
The project has been developed under the MIT license.


Related Sitecore Projects
--------------------------------
- [Solr for Sitecore](https://github.com/vasiliyfomichev/solr-for-sitecore) - pre-built Solr Docker images ready to be used with Sitecore out of the box.
- [Sitecore ADFS Authenticator Module](https://github.com/vasiliyfomichev/Sitecore-ADFS-Authenticator-Module) - Sitecore module for authenticating users using ADFS.
- [Sitecore Lucene Term Highlighter](https://github.com/vasiliyfomichev/Sitecore-Solr-Search-Term-Highlight) - enables search term highlighting in Sitecore search results when used with Lucene.



Copyright 2015 Vasiliy Fomichev
