using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace browserHistoryBrowser
{
    internal static class ConfigureService
    {
        internal static void Configure()
        {
            HostFactory.Run(configure =>
            {
                configure.Service<browserHistoryReader>(service =>
                {
                    service.ConstructUsing(s => new browserHistoryReader());
                    service.WhenStarted(s => s.Start());
                    service.WhenStopped(s => s.Stop());
                });
                //Setup Account that window service use to run.  
                configure.RunAsLocalSystem();
                configure.SetServiceName("MyWindowServiceWithTopshelf");
                configure.SetDisplayName("MyWindowServiceWithTopshelf");
                configure.SetDescription("My .Net windows service with Topshelf");
            });
        }
    }
}
