using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using NUnit.Framework;
using Ninject.Extensions.Wcf.Client.Test.Mock;

namespace Ninject.Extensions.Wcf.Client.Test.Unit
{
    [TestFixture(Category = "Unit,Fast")]
    public class NinjectWcfClientExtensionsTests
    {
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
    }
}
