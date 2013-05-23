Ninject Extensions for WCF Clients
================

Tired of Service References? Want to use dependency injection (IoC)? Ninject Extensions for WCF Clients lets you bind your service interfaces to a WCF service channel with a single line of code for each interface.

Usage
=

1. If you haven't already, add [Ninject](http://www.ninject.org/) to your client project.
2. Reference Ninject.Extensions.Wcf.Client in your client project. Available on [Nuget](https://nuget.org/packages/Ninject.Extensions.Wcf.Client/).
3. Ensure your service contracts (interfaces only) and data contracts (interfaces and implementations) are in a library referenced by both your client and server projects.
4. Ensure your client's Web.config or App.config has an [endpoint](http://msdn.microsoft.com/en-us/library/ms731144.aspx) for each service.
5. Add the following code to your client's [NinjectModule](https://github.com/ninject/ninject/wiki/Modules-and-the-Kernel):

        using Ninject.Extensions.Wcf.Client;
        
        ...
        
        Bind<IMyService1>.ToServiceChannel();
        Bind<IMyService2>.ToServiceChannel();

6. Consume the service:

        private readonly IMyService1 myService1;
        
        public SomeClass(IMyService1 myService1) {
            this.myService1 = myService1;
        }
        
        ...
        
        myService1.DoSomething(); // Calls the service method over a WCF channel

And that's it! No Service References required. Have a look at the [example projects](https://github.com/cyanfish/ninject.extensions.wcf.client/tree/master/src/Examples) if you like.

Notes
=

- If your client is a web project, preferably use [InRequestScope](https://github.com/ninject/Ninject.Web.Common/wiki/InRequestScope):

        Bind<IMyService1>.ToServiceChannel().InRequestScope();

- The channel object returned when Ninject resolves a service interface is NOT thread-safe, and will eventually timeout:

        // DO NOT DO THIS
        Bind<IMyService1>.ToServiceChannel().InSingletonScope(); // VERY BAD IDEA

- If you have multiple endpoints in your Web.config or App.config for the same service contract, you can bind by name:

        Bind<IMyService1>.ToServiceChannel("IMyService1_BasicHttpBinding");
        
  Similarly, you can use other overloads to programmatically specify an endpoint.
