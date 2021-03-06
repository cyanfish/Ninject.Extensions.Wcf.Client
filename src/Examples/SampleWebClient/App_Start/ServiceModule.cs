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
using Ninject.Extensions.Wcf.Client;
using Ninject.Modules;
using Ninject.Web.Common;
using SampleContracts;

namespace SampleWebClient.App_Start
{
    class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            // Declare your WCF service bindings here
            Bind<IService1>().ToServiceChannel().InRequestScope();
        }
    }
}