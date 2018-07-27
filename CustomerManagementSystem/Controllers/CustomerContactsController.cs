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
    public class CustomerContactsController : Controller
    {
        private I客戶聯絡人Repository ContactsRepo;
        private I客戶資料Repository CustomerRepo;

        public CustomerContactsController()
        {
            this.ContactsRepo = RepositoryHelper.Get客戶聯絡人Repository();
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository(this.ContactsRepo.UnitOfWork);
        }

        public ActionResult Index(CustomerContactsQueryViewModel data=null)
        {
            CustomerContactsQueryViewModel result = new CustomerContactsQueryViewModel();
            result.Contacts = ContactsRepo.Search(data.Query, data.Paging, data.Sort);
            result.Paging.Count = ContactsRepo.SearchCount(data.Query);
            result.Paging.Take = data.Paging.Take;
            result.Query = data.Query;
            result.BatchEdit = data.BatchEdit;
            return View(result);
        }

        [HttpPost]
        public ActionResult GetExcel(CustomerContactsQueryViewModel cond = null)
        {

            using (var wb = GetExcelFile(cond))
            {
                // Add ClosedXML.Extensions in your using declarations
                return wb.Deliver($"ExportCustomerContacts_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            }
        }

        private XLWorkbook GetExcelFile(CustomerContactsQueryViewModel cond = null)
        {
            List<CustomerContactViewModel> list = ContactsRepo.SearchAll(cond.Query); ;
            return ClosedXmlHelper.ToClosedXmlExcel(list);
        }

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶聯絡人 客戶聯絡人 = ContactsRepo.GetContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                throw new DataNotFoundException();
            }
            return View(客戶聯絡人);
        }

        // GET: CustomerContacts/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: CustomerContacts/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                ContactsRepo.Add(客戶聯絡人);
                ContactsRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: CustomerContacts/Edit/5
        [HandleError(ExceptionType =typeof(DataNotFoundException),View ="Error")]
        [HandleError(ExceptionType =typeof(NoPrimaryKeyPassException),View ="Error")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶聯絡人 客戶聯絡人 = ContactsRepo.GetContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                throw new DataNotFoundException();
            }
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: CustomerContacts/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(CustomerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶聯絡人 客戶聯絡人 = ContactsRepo.GetContactById(id.Value);
            if (客戶聯絡人 == null)
            {
                throw new DataNotFoundException();
            }
            return View(客戶聯絡人);
        }

        // POST: CustomerContacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = ContactsRepo.GetContactById(id);
            ContactsRepo.Delete(客戶聯絡人);
            ContactsRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BatchEdit(List<CustomerContactViewModel> list)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }
            try
            {
                ContactsRepo.BatchUpdate(list);
                ContactsRepo.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("error", ex);
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ContactsRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
