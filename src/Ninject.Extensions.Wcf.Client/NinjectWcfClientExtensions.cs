// 
// Copyright (c) 2013 Ben Olden-Cooligan
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax) where TContract : class
        {
            return ToServiceChannel(bindingSyntax, "*");
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the endpoint declared in the Web.config or App.config file with the given name.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <param name="endpointConfigurationName">The configuration name used for the endpoint.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpointConfigurationName"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            string endpointConfigurationName) where TContract : class
        {
            ThrowIfNull(endpointConfigurationName, "endpointConfigurationName");

            return BindChannel(bindingSyntax, () => new ChannelFactory<TContract>(endpointConfigurationName));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using given endpoint address and the binding of the endpoint declared in the Web.config or App.config file with the given name.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <param name="endpointConfigurationName">The configuration name used for the endpoint.</param>
        /// <param name="remoteAddress">The <see cref="T:System.ServiceModel.EndpointAddress"/> that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpointConfigurationName"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            string endpointConfigurationName, string remoteAddress) where TContract : class
        {
            ThrowIfNull(endpointConfigurationName, "endpointConfigurationName");
            ThrowIfNull(remoteAddress, "remoteAddress");

            return BindChannel(bindingSyntax, () => new ChannelFactory<TContract>(endpointConfigurationName, new EndpointAddress(remoteAddress)));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using given endpoint address and the binding of the endpoint declared in the Web.config or App.config file with the given name.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <param name="endpointConfigurationName">The configuration name used for the endpoint.</param>
        /// <param name="remoteAddress">The <see cref="T:System.ServiceModel.EndpointAddress"/> that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="endpointConfigurationName"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            string endpointConfigurationName, EndpointAddress remoteAddress) where TContract : class
        {
            ThrowIfNull(endpointConfigurationName, "endpointConfigurationName");
            ThrowIfNull(remoteAddress, "remoteAddress");

            return BindChannel(bindingSyntax, () => new ChannelFactory<TContract>(endpointConfigurationName, remoteAddress));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <param name="binding">The <see cref="T:System.ServiceModel.Channels.Binding"/> used to configure the endpoint.</param>
        /// <param name="remoteAddress">The address that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="binding"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            Binding binding, string remoteAddress) where TContract : class
        {
            ThrowIfNull(binding, "binding");
            ThrowIfNull(remoteAddress, "remoteAddress");

            return BindChannel(bindingSyntax, () => new ChannelFactory<TContract>(binding, new EndpointAddress(remoteAddress)));
        }

        /// <summary>
        /// Indicates that the service should be bound to a WCF service channel using the given binding and the endpoint declared in the Web.config or App.config file.
        /// </summary>
        /// <typeparam name="TContract">The service contract interface type.</typeparam>
        /// <param name="bindingSyntax">The result from Bind&lt;TContract&gt;().</param>
        /// <param name="binding">The <see cref="T:System.ServiceModel.Channels.Binding"/> used to configure the endpoint.</param>
        /// <param name="remoteAddress">The <see cref="T:System.ServiceModel.EndpointAddress"/> that provides the location of the service.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="binding"/> or <paramref name="remoteAddress"/> is null.</exception>
        /// <returns>The fluent syntax;</returns>
        public static IBindingWhenInNamedWithOrOnSyntax<TContract> ToServiceChannel<TContract>(
            this IBindingToSyntax<TContract> bindingSyntax,
            Binding binding, EndpointAddress remoteAddress) where TContract : class
        {
            ThrowIfNull(binding, "binding");
            ThrowIfNull(remoteAddress, "remoteAddress");

            return BindChannel(bindingSyntax, () => new ChannelFactory<TContract>(binding, remoteAddress));
        }

        private static IBindingWhenInNamedWithOrOnSyntax<TContract> BindChannel<TContract>(
            IBindingToSyntax<TContract> bindingSyntax, Func<ChannelFactory<TContract>> factoryFactory) where TContract : class
        {
            // Don't create the ChannelFactory at binding time, since it's an expensive operation
            var lazyFactory = new Lazy<ChannelFactory<TContract>>(factoryFactory);
            return bindingSyntax.ToMethod(x => lazyFactory.Value.CreateChannel());
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
