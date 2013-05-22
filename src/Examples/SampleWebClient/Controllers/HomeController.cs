using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SampleContracts;
using SampleWebClient.Models;

namespace SampleWebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IService1 service1;

        public HomeController(IService1 service1)
        {
            this.service1 = service1;
        }

        public ActionResult Index()
        {
            return View(new HomeModel());
        }

        [HttpPost]
        public ActionResult Index(int intValue, bool boolValue, string stringValue)
        {
            return View(new HomeModel
            {
                Response1 = service1.GetData(intValue),
                Response2 = service1.GetDataUsingDataContract(new CompositeType { BoolValue = boolValue, StringValue = stringValue })
            });
        }

    }
}
