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


namespace CustomerManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private I客戶資料Repository CustomerRepo;

        public CustomersController()
        {
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository();
        }
        // GET: Customers
        public ActionResult Index()
        {
            CustomersQueryViewModel result = new CustomersQueryViewModel();
            result.Customers = CustomerRepo.Search(result.Query, result.Paging).Select(x => new CustomersViewModel()
            {
                Address = x.地址,
                ClientName = x.客戶名稱,
                CompanyNumber = x.統一編號,
                Email = x.Email,
                Fax = x.傳真,
                Phone = x.電話
            }).ToList();
            result.Paging.Count = CustomerRepo.SearchCount(result.Query);
            result.Paging.Skip = result.Paging.Skip;
            result.Paging.Take = result.Paging.Take;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(CustomersQueryViewModel cond =null)
        {
            CustomersQueryViewModel result=new CustomersQueryViewModel();
            result.Customers = CustomerRepo.Search(cond.Query,cond.Paging).Select(x=>new CustomersViewModel() {
                Address=x.地址,
                ClientName=x.客戶名稱,
                CompanyNumber=x.統一編號,
                Email=x.Email,
                Fax=x.傳真,
                Phone=x.電話
            }).ToList();
            result.Paging.Count = CustomerRepo.SearchCount(cond.Query);
            result.Paging.Skip = cond.Paging.Skip;
            result.Paging.Take = cond.Paging.Take;
            result.Query = cond.Query;
            return View(result);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.Add(客戶資料);
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id);
            CustomerRepo.Delete(客戶資料);
            CustomerRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CustomerRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
