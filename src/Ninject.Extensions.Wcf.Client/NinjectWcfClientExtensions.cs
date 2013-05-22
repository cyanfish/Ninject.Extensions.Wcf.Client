using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using Ninject;
using Ninject.Syntax;

namespace Ninject.Extensions.Wcf.Client
{
    public static class NinjectWcfClientExtensions
    {
        public static IBindingWhenInNamedWithOrOnSyntax<T> ToServiceChannel<T>(this IBindingToSyntax<T> syntax,
                                                                               string endpointConfigurationName = null)
        {
            BindChannelFactory<T>(syntax.Kernel, endpointConfigurationName);
            return syntax.ToMethod(x => syntax.Kernel.Get<ChannelFactory<T>>(endpointConfigurationName).CreateChannel());
        }

        private static void BindChannelFactory<T>(IKernel kernel, string endpointConfigurationName)
        {
            var factoryBinding = kernel.Bind<ChannelFactory<T>>().ToSelf().InSingletonScope();
            if (endpointConfigurationName != null)
            {
                factoryBinding.Named(endpointConfigurationName);
            }
            factoryBinding.WithConstructorArgument("endpointConfigurationName", endpointConfigurationName);
        }
    }
}
