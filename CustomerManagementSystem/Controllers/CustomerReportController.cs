using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.Controllers
{
    public class CustomerReportController : Controller
    {
        private I客戶資料Repository CustomerRepo;

        public CustomerReportController()
        {
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository();
        }

        // GET: CustomerReport
        public ActionResult Index()
        {
            var list = CustomerRepo.GetReport();
            return View(list);
        }
    }
}