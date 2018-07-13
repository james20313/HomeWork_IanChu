using ClosedXML.Excel;
using ClosedXML.Extensions;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

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

        [HttpPost]
        public ActionResult GetExcel()
        {

            using (var wb = GetExcelFile())
            {
                // Add ClosedXML.Extensions in your using declarations
                return wb.Deliver($"ExportCustomerReport_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            }
        }

        private XLWorkbook GetExcelFile()
        {
            List<CustomerReportViewModel> list = CustomerRepo.GetReport();
            return ClosedXmlHelper.ToClosedXmlExcel(list);
        }
    }
}