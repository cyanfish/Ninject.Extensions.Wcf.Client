using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
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

        [Test]
        public void ToServiceChannel_NoArguments_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel();
            AssertChannelFactoryBindingResolved<IMockInterface1>(42, "http://localhost/TestService1.svc");
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
        public void ToServiceChannel_ExistingConfigName_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1");
            AssertChannelFactoryBindingResolved<IMockInterface1>(42, "http://localhost/TestService1.svc");
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
        public void ToServiceChannel_ExistingConfigNameAndValidAddressString_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", "http://localhost/TestService2.svc");
            AssertChannelFactoryBindingResolved<IMockInterface1>(42, "http://localhost/TestService2.svc");
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
        public void ToServiceChannel_ExistingConfigNameAndValidEndpointAddress_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel("TestEndpoint1", new EndpointAddress("http://localhost/TestService2.svc"));
            AssertChannelFactoryBindingResolved<IMockInterface1>(42, "http://localhost/TestService2.svc");
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
        public void ToServiceChannel_SpecifiedChannelBindingAndValidAddressString_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, "http://localhost/TestService2.svc");
            AssertChannelFactoryBindingResolved<IMockInterface1>(43, "http://localhost/TestService2.svc");
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
        public void ToServiceChannel_SpecifiedChannelBindingAndValidEndpointAddress_ResolvesFactoryBinding()
        {
            kernel.Bind<IMockInterface1>().ToServiceChannel(new BasicHttpBinding { OpenTimeout = TimeSpan.FromSeconds(43) }, new EndpointAddress("http://localhost/TestService2.svc"));
            AssertChannelFactoryBindingResolved<IMockInterface1>(43, "http://localhost/TestService2.svc");
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


        #region Helpers

        private void AssertChannelBindingResolved<T>()
        {
            var result = kernel.Get<T>();
            Assert.That(result, Is.InstanceOf<IClientChannel>());
        }

        private void AssertChannelFactoryBindingResolved<T>(int openTimeoutSeconds, string endpointAddress)
        {
            var result = kernel.Get<ChannelFactory<T>>();
            Assert.That(result.Endpoint.Binding.OpenTimeout.TotalSeconds, Is.EqualTo(openTimeoutSeconds));
            Assert.That(result.Endpoint.Address.Uri.ToString(), Is.EqualTo(endpointAddress));
        }

        #endregion
    }
}
