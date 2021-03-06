﻿// 
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
using System.ServiceModel.Channels;
using NUnit.Framework;
using Ninject.Extensions.Wcf.Client.Test.Mock;

namespace Ninject.Extensions.Wcf.Client.Test.Unit
{
    [TestFixture(Category = "Unit,Fast")]
    public class NinjectWcfClientExtensionsTests
    {
        #region Shared

        private IKernel kernel;

        [SetUp]
        public void SetUp()
        {
            kernel = new StandardKernel();
        }

        [TearDown]
        public void TearDown()
        {
            kernel.Dispose();
            kernel = null;
        }

        #endregion

        #region ToServiceChannel() tests

        [Test]
        public void ToServiceChannel_NoArguments_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel();
            AssertChannelBindingResolved<IMockInterface1>();
        }

        #endregion

        #region ToServiceChannel(string) tests

        [Test]
        public void ToServiceChannel_ExistingConfigName_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1");
            AssertChannelBindingResolved<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToServiceChannel_NonExistingConfigName_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint2");
            kernel.Get<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_NullConfigName_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel((string)null);
        }

        #endregion

        #region ToServiceChannel(string, string) tests

        [Test]
        public void ToServiceChannel_ExistingConfigNameAndValidAddressString_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", "http://localhost/TestService2.svc");
            AssertChannelBindingResolved<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToServiceChannel_NonExistingConfigNameAndValidAddressString_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint2", "http://localhost/TestService2.svc");
            kernel.Get<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_NullConfigNameAndValidAddressString_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel((string)null, "http://localhost/TestService2.svc");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_ExistingConfigNameAndNullAddressString_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint2", (string)null);
        }

        #endregion

        #region ToServiceChannel(string, EndpointAddress) tests

        [Test]
        public void ToServiceChannel_ExistingConfigNameAndValidEndpointAddress_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", new EndpointAddress("http://localhost/TestService2.svc"));
            AssertChannelBindingResolved<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToServiceChannel_NonExistingConfigNameAndValidEndpointAddress_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint2", new EndpointAddress("http://localhost/TestService2.svc"));
            kernel.Get<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_NullConfigNameAndValidEndpointAddress_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel((string)null, new EndpointAddress("http://localhost/TestService2.svc"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_ExistingConfigNameAndNullEndpointAddress_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint2", (EndpointAddress)null);
        }

        #endregion

        #region ToServiceChannel(Binding, string) tests

        [Test]
        public void ToServiceChannel_SpecifiedChannelBindingAndValidAddressString_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, "http://localhost/TestService2.svc");
            AssertChannelBindingResolved<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_NullChannelBindingAndValidAddressString_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel((Binding)null, "http://localhost/TestService2.svc");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_SpecifiedChannelBindingAndNullAddressString_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, (string)null);
        }

        #endregion

        #region ToServiceChannel(Binding, EndpointAddress) tests

        [Test]
        public void ToServiceChannel_SpecifiedChannelBindingAndValidEndpointAddress_ResolvesBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, new EndpointAddress("http://localhost/TestService2.svc"));
            AssertChannelBindingResolved<IMockInterface1>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_NullChannelBindingAndValidEndpointAddress_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel((Binding)null, new EndpointAddress("http://localhost/TestService2.svc"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToServiceChannel_SpecifiedChannelBindingAndNullEndpointAddress_Throws()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, (EndpointAddress)null);
        }

        #endregion

        #region Common tests

        [Test]
        public void ToServiceChannel_MultipleOverloadUse_ResolvesFactoryUnambiguously()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel().Named("1");
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1").Named("2");
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", "http://localhost/TestService1.svc").Named("3");
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", new EndpointAddress("http://localhost/TestService1.svc")).Named("4");
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(42) }, "http://localhost/TestService1.svc").Named("5");
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(42) }, new EndpointAddress("http://localhost/TestService1.svc")).Named("6");

            AssertChannelBindingResolved<IMockInterface1>("1");
            AssertChannelBindingResolved<IMockInterface1>("2");
            AssertChannelBindingResolved<IMockInterface1>("3");
            AssertChannelBindingResolved<IMockInterface1>("4");
            AssertChannelBindingResolved<IMockInterface1>("5");
            AssertChannelBindingResolved<IMockInterface1>("6");
        }

        #endregion

        #region Helpers

        private void AssertChannelBindingResolved<T>()
        {
            var result = kernel.Get<T>();
            Assert.That(result, Is.InstanceOf<IClientChannel>());
        }

        private void AssertChannelBindingResolved<T>(string name)
        {
            var result = kernel.Get<T>(name);
            Assert.That(result, Is.InstanceOf<IClientChannel>());
        }

        #endregion
    }
}
