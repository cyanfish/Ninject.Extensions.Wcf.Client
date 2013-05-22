using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ninject.Extensions.Wcf.Client.Test.Mock
{
    [ServiceContract]
    interface IMockInterface1
    {
        [OperationContract]
        void DoSomething();
    }

    [ServiceContract]
    interface IMockInterface2
    {
        [OperationContract]
        void DoSomething();
    }

    [ServiceContract]
    interface IMockInterface3
    {
        [OperationContract]
        void DoSomething();
    }

    [ServiceContract]
    interface IMockInterface4
    {
        [OperationContract]
        void DoSomething();
    }

    [ServiceContract]
    interface IMockInterface5
    {
        [OperationContract]
        void DoSomething();
    }

    [ServiceContract]
    interface IMockInterface6
    {
        [OperationContract]
        void DoSomething();
    }
}
