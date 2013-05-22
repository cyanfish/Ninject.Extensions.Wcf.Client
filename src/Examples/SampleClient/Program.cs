using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Ninject;
using SampleContracts;

namespace SampleWcfClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new ServiceModule());
            kernel.Get<Program>().Run();
        }

        private readonly IService1 service1;

        public Program(IService1 service1)
        {
            // Ninject injects the service1 dependency
            this.service1 = service1;
        }

        private void Run()
        {
            // Method calls on IService1 are executed through the WCF service channel
            Console.WriteLine(service1.GetData(10));
            Console.WriteLine(service1.GetDataUsingDataContract(new CompositeType { BoolValue = true, StringValue = "Hello world!" }));

            // Service channels aren't meant to last very long - once you're done with them, you should dispose them.
            // If they aren't disposed, they will timeout after a while.
            // If you specify a scope on the binding (e.g. InRequestScope() for a web project), Ninject will handle the disposal.
            if (service1 is IDisposable)
            {
                ((IDisposable)service1).Dispose();
            }

            Console.ReadLine();
        }

        private class Client1 : ClientBase<IService1>
        {

        }
    }
}
