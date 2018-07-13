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

namespace CustomerManagementSystem.Controllers
{
    public class CustomersController : Controller
    {
        private I客戶資料Repository CustomerRepo;
        private I客戶類別Repository CustomerTypeRepo;

        public CustomersController()
        {
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository();
            this.CustomerTypeRepo = RepositoryHelper.Get客戶類別Repository();
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
                CustomerTypeName = x.客戶類別.類別名稱,
                Email = x.Email,
                Fax = x.傳真,
                Phone = x.電話,
                Id = x.Id
            }).ToList();
            result.CustomerTypeList = CustomerTypeRepo.All().Select(x => new SelectListItem()
            {
                Text = x.類別名稱,
                Value = x.Id.ToString(),
                Selected = x.Id == 0 ? true : false
            }).ToList();
            result.Paging.Count = CustomerRepo.SearchCount(result.Query);
            result.Paging.Skip = result.Paging.Skip;
            result.Paging.Take = result.Paging.Take;
            return View(result);
        }

        [HttpPost]
        public ActionResult Index(CustomersQueryViewModel cond = null)
        {
            CustomersQueryViewModel result = new CustomersQueryViewModel();
            result.Customers = CustomerRepo.Search(cond.Query, cond.Paging).Select(x => new CustomersViewModel()
            {
                Address = x.地址,
                ClientName = x.客戶名稱,
                CompanyNumber = x.統一編號,
                CustomerTypeName = x.客戶類別.類別名稱,
                Email = x.Email,
                Fax = x.傳真,
                Phone = x.電話,
                Id = x.Id
            }).ToList();
            result.CustomerTypeList = CustomerTypeRepo.All().ToList().Select(x => new SelectListItem()
            {
                Text = x.類別名稱,
                Value = x.Id.ToString(),
                Selected = cond != null ?
                               cond.Query.CustomerTypeId.HasValue ?
                                   x.Id == cond.Query.CustomerTypeId.Value ? true : false
                               : false
                           : false
            }).ToList();
            result.Paging.Count = CustomerRepo.SearchCount(cond.Query);
            result.Paging.Skip = cond.Paging.Skip;
            result.Paging.Take = cond.Paging.Take;
            result.Query = cond.Query;
            return View(result);
        }

        [HttpPost]
        public ActionResult GetExcel(CustomersQueryViewModel cond = null)
        {
            
            using (var wb = GetExcelFile(cond))
            {
                // Add ClosedXML.Extensions in your using declarations
                return wb.Deliver($"ExportCustomer_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            }
        }

        private XLWorkbook GetExcelFile(CustomersQueryViewModel cond = null)
        {
            List<CustomersViewModel> list= CustomerRepo.Search(cond.Query, cond.Paging).Select(x => new CustomersViewModel()
            {
                Address = x.地址,
                ClientName = x.客戶名稱,
                CompanyNumber = x.統一編號,
                CustomerTypeName = x.客戶類別.類別名稱,
                Email = x.Email,
                Fax = x.傳真,
                Phone = x.電話,
                Id = x.Id
            }).ToList();
            return ClosedXmlHelper.ToClosedXmlExcel(list);
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
            CustomerDetailViewModel vm = new CustomerDetailViewModel()
            {
                CustomerTypeName = 客戶資料.客戶類別.類別名稱,
                Email = 客戶資料.Email,
                Id = 客戶資料.Id,
                傳真 = 客戶資料.傳真,
                地址 = 客戶資料.地址,
                客戶名稱 = 客戶資料.客戶名稱,
                統一編號 = 客戶資料.統一編號,
                電話 = 客戶資料.電話
            };
            return View(vm);
        }

        private CustomerEditViewModel InitCustomerEditViewModel(客戶資料 existData = null)
        {
            var initData = new CustomerEditViewModel()
            {
                Customer = new 客戶資料() { 類別Id = 1 },
                CustomerTypeList = CustomerTypeRepo.All().Select(x => new SelectListItem()
                {
                    Text = x.類別名稱,
                    Value = x.Id.ToString(),
                    Selected = x.Id == 1 ? true : false
                }).ToList()
            };
            if (existData != null)
            {
                initData.Customer = existData;
            }
            return initData;
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View(InitCustomerEditViewModel());
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                客戶資料 newData = vm.Customer;
                CustomerRepo.Add(newData);
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(vm);
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

            return View(InitCustomerEditViewModel(客戶資料));
        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                客戶資料 editData = vm.Customer;
                CustomerRepo.UnitOfWork.Context.Entry(editData).State = EntityState.Modified;
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(vm);
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
            CustomerDetailViewModel vm = new CustomerDetailViewModel()
            {
                CustomerTypeName = 客戶資料.客戶類別.類別名稱,
                Email = 客戶資料.Email,
                Id = 客戶資料.Id,
                傳真 = 客戶資料.傳真,
                地址 = 客戶資料.地址,
                客戶名稱 = 客戶資料.客戶名稱,
                統一編號 = 客戶資料.統一編號,
                電話 = 客戶資料.電話
            };
            return View(vm);
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
