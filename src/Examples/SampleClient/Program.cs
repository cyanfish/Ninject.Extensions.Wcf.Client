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
            // Method calls on service1 are executed through the WCF service channel
            Console.WriteLine(service1.GetData(10));
            Console.WriteLine(service1.GetDataUsingDataContract(new CompositeType { BoolValue = true, StringValue = "Hello world!" }).StringValue);

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
