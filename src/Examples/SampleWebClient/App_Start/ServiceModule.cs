using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Extensions.Wcf.Client;
using Ninject.Modules;
using Ninject.Web.Common;
using SampleContracts;

namespace SampleWebClient.App_Start
{
    class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            // Declare your WCF service bindings here
            Bind<IService1>().ToServiceChannel().InRequestScope();
        }
    }
}