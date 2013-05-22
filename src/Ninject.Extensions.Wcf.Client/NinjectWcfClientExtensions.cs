using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using Binding = System.ServiceModel.Channels.Binding;

namespace Ninject.Extensions.Wcf.Client
{
    public static class NinjectWcfClientExtensions
    {
        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax)
        {
            BindChannelFactory(bindingSyntax, null, null, null, null, null);
            return BindChannel(bindingSyntax, x => !x.Has("binding") && !x.Has("endpoint")
                                                   && !x.Has("endpointConfigurationName") && !x.Has("remoteAddress"));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the endpoint declared in the Web.config or App.config file with the given name.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="endpointConfigurationName">The configuration name used for the endpoint.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpointConfigurationName"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            string endpointConfigurationName)
        {
            ThrowIfNull(endpointConfigurationName, "endpointConfigurationName");

            BindChannelFactory(bindingSyntax, null, null, endpointConfigurationName, null, null);
            return BindChannel(bindingSyntax,
                               x => x.Get<string>("endpointConfigurationName") == endpointConfigurationName
                                    && !x.Has("remoteAddress"));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using given endpoint address and the binding of the endpoint declared in the Web.config or App.config file with the given name.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="endpointConfigurationName">The configuration name used for the endpoint.</param>
        /// <param name="remoteAddress">The <see cref="T:System.ServiceModel.EndpointAddress"/> that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpointConfigurationName"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            string endpointConfigurationName, EndpointAddress remoteAddress)
        {
            ThrowIfNull(endpointConfigurationName, "endpointConfigurationName");
            ThrowIfNull(remoteAddress, "remoteAddress");

            BindChannelFactory(bindingSyntax, null, null, endpointConfigurationName, remoteAddress, null);
            return BindChannel(bindingSyntax,
                               x => x.Get<string>("endpointConfigurationName") == endpointConfigurationName
                                    && ReferenceEquals(x.Get<object>("remoteAddress"), remoteAddress));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="binding">The <see cref="T:System.ServiceModel.Channels.Binding"/> used to configure the endpoint.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="binding"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            Binding binding)
        {
            ThrowIfNull(binding, "binding");

            BindChannelFactory(bindingSyntax, binding, null, null, null, null);
            return BindChannel(bindingSyntax,
                               x => x.Get<Binding>("binding") == binding
                                    && !x.Has("remoteAddress"));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="binding">The <see cref="T:System.ServiceModel.Channels.Binding"/> used to configure the endpoint.</param>
        /// <param name="remoteAddress">The address that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="binding"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            Binding binding, string remoteAddress)
        {
            ThrowIfNull(binding, "binding");
            ThrowIfNull(remoteAddress, "remoteAddress");

            BindChannelFactory(bindingSyntax, binding, null, null, null, remoteAddress);
            return BindChannel(bindingSyntax,
                               x => x.Get<Binding>("binding") == binding
                                    && ReferenceEquals(x.Get<object>("remoteAddress"), remoteAddress));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="binding">The <see cref="T:System.ServiceModel.Channels.Binding"/> used to configure the endpoint.</param>
        /// <param name="remoteAddress">The <see cref="T:System.ServiceModel.EndpointAddress"/> that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="binding"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            Binding binding, EndpointAddress remoteAddress)
        {
            ThrowIfNull(binding, "binding");
            ThrowIfNull(remoteAddress, "remoteAddress");

            BindChannelFactory(bindingSyntax, binding, null, null, remoteAddress, null);
            return BindChannel(bindingSyntax,
                               x => x.Get<Binding>("binding") == binding
                                    && ReferenceEquals(x.Get<object>("remoteAddress"), remoteAddress));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;T&gt;().</param>
        /// <param name="endpoint">The <see cref="T:System.ServiceModel.Description.ServiceEndpoint"/> for the channels produced by the factory.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpoint"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            ServiceEndpoint endpoint)
        {
            ThrowIfNull(endpoint, "endpoint");

            BindChannelFactory(bindingSyntax, null, endpoint, null, null, null);
            return BindChannel(bindingSyntax,
                               x => x.Get<ServiceEndpoint>("endpoint") == endpoint);
        }

        private static IBindingWhenInNamedWithOrOnSyntax<TContract> BindChannel<TContract>(
            IBindingToSyntax<TContract> bindingSyntax, Func<IBindingMetadata, bool> constraint)
        {
            return bindingSyntax.ToMethod(x => bindingSyntax.Kernel.Get<ChannelFactory<TContract>>(constraint).CreateChannel());
        }

        private static void BindChannelFactory<TContract>(IBindingToSyntax<TContract> bindingSyntax, Binding binding,
                                                          ServiceEndpoint endpoint, string endpointConfigurationName,
                                                          EndpointAddress remoteAddress, string remoteAddressStr)
        {
            var factoryBinding = bindingSyntax.Kernel.Bind<ChannelFactory<TContract>>().ToSelf().InSingletonScope();
            AddParam(factoryBinding, "binding", binding);
            AddParam(factoryBinding, "endpoint", endpoint);
            AddParam(factoryBinding, "endpointConfigurationName", endpointConfigurationName);
            AddParam(factoryBinding, "remoteAddress", remoteAddress);
            AddParam(factoryBinding, "remoteAddress", remoteAddressStr);
        }

        private static void AddParam<TContract>(IBindingNamedWithOrOnSyntax<ChannelFactory<TContract>> factoryBinding,
                                                string paramName, object paramValue)
        {
            if (paramValue != null)
            {
                factoryBinding.WithMetadata(paramName, paramValue);
                factoryBinding.WithConstructorArgument(paramName, paramValue);
            }
        }

        private static void ThrowIfNull(object param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
