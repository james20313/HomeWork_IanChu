using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagementSystem.Models;
using CustomerManagementSystem.ViewModels;
using ClosedXML.Excel;
using WebApplication1.Models;
using ClosedXML.Extensions;
using CustomerManagementSystem.Models.Exceptions;

namespace CustomerManagementSystem.Controllers
{
    public class BankAccountsController : Controller
    {
        private I客戶銀行資訊Repository BankAccountsRepo;
        private I客戶資料Repository CustomerRepo;

        public BankAccountsController()
        {
            this.BankAccountsRepo = RepositoryHelper.Get客戶銀行資訊Repository();
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository(this.BankAccountsRepo.UnitOfWork);
        }

        // GET: BankAccounts
        public ActionResult Index()
        {
            BankAccountQueryViewModel result = new BankAccountQueryViewModel();
            result.BankAccounts = BankAccountsRepo.Search(result.Query, result.Paging);
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(BankAccountQueryViewModel data = null)
        {
            BankAccountQueryViewModel result = new BankAccountQueryViewModel();
            result.BankAccounts = BankAccountsRepo.Search(data.Query, data.Paging);
            result.Paging.Count = BankAccountsRepo.SearchCount(data.Query);
            result.Paging.Skip = data.Paging.Skip;
            result.Paging.Take = data.Paging.Take;
            result.Query = data.Query;
            return View(result);
        }

        [HttpPost]
        public ActionResult GetExcel(BankAccountQueryViewModel cond = null)
        {

            using (var wb = GetExcelFile(cond))
            {
                // Add ClosedXML.Extensions in your using declarations
                return wb.Deliver($"ExportCustomerBankAccount_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            }
        }

        private XLWorkbook GetExcelFile(BankAccountQueryViewModel cond = null)
        {
            List<BankAccountViewModel> list = BankAccountsRepo.Search(cond.Query);
            return ClosedXmlHelper.ToClosedXmlExcel(list);
        }

        // GET: BankAccounts/Details/5
        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶銀行資訊 客戶銀行資訊 = BankAccountsRepo.GetBankAccountById(id.Value);
            if (客戶銀行資訊 == null)
            {
                throw new DataNotFoundException();
            }
            return View(客戶銀行資訊);
        }

        // GET: BankAccounts/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: BankAccounts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                BankAccountsRepo.Add(客戶銀行資訊);
                BankAccountsRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶銀行資訊 客戶銀行資訊 = BankAccountsRepo.GetBankAccountById(id.Value);
            if (客戶銀行資訊 == null)
            {
                throw new DataNotFoundException();
            }
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: BankAccounts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                BankAccountsRepo.UnitOfWork.Context.Entry(客戶銀行資訊).State = EntityState.Modified;
                BankAccountsRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶銀行資訊 客戶銀行資訊 = BankAccountsRepo.GetBankAccountById(id.Value);
            if (客戶銀行資訊 == null)
            {
                throw new DataNotFoundException();
            }
            return View(客戶銀行資訊);
        }

        // POST: BankAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶銀行資訊 客戶銀行資訊 = BankAccountsRepo.GetBankAccountById(id);
            BankAccountsRepo.Delete(客戶銀行資訊);
            BankAccountsRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BankAccountsRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
