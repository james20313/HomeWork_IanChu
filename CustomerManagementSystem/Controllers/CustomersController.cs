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
using X.PagedList;
using System.Text;
using System.Security.Cryptography;

namespace CustomerManagementSystem.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private I客戶資料Repository CustomerRepo;
        private I客戶類別Repository CustomerTypeRepo;
        private I客戶聯絡人Repository ContactsRepo;

        public CustomersController()
        {
            this.CustomerRepo = RepositoryHelper.Get客戶資料Repository();
            this.CustomerTypeRepo = RepositoryHelper.Get客戶類別Repository(this.CustomerRepo.UnitOfWork);
            this.ContactsRepo = RepositoryHelper.Get客戶聯絡人Repository(this.CustomerRepo.UnitOfWork);
        }
        
        [Authorize(Roles ="Manager")]
        public ActionResult Index(CustomersQueryViewModel cond = null)
        {
            CustomersQueryViewModel result = new CustomersQueryViewModel();
            if (cond == null)
            {
                //預設排序
                result.Sort.Column = "Id";
                result.Sort.Sort = "desc";
            }
            var customerList = CustomerRepo.Search(cond.Query, cond.Paging, cond.Sort).Select(x => new CustomersViewModel()
            {
                Address = x.地址,
                ClientName = x.客戶名稱,
                CompanyNumber = x.統一編號,
                CustomerTypeName = x.客戶類別.類別名稱,
                Email = x.Email,
                Fax = x.傳真,
                Phone = x.電話,
                Id = x.Id
            });
            result.Paging.Count = CustomerRepo.SearchCount(cond.Query);
            result.Customers = new StaticPagedList<CustomersViewModel>(customerList,cond.Paging.Page,cond.Paging.Take, result.Paging.Count);
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
            result.Paging.Take = cond.Paging.Take;
            result.Query = cond.Query;
            return View(result);
        }

        [HttpPost]
        [Authorize(Roles ="Manager")]
        public ActionResult GetExcel(CustomersQueryViewModel cond = null)
        {
            
            using (var wb = GetExcelFile(cond))
            {
                // Add ClosedXML.Extensions in your using declarations
                return wb.Deliver($"ExportCustomer_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx");
            }
        }

        [Authorize(Roles ="Manager")]
        private XLWorkbook GetExcelFile(CustomersQueryViewModel cond = null)
        {
            List<CustomersViewModel> list= CustomerRepo.Search(cond.Query, cond.Paging, cond.Sort).Select(x => new CustomersViewModel()
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

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        [Authorize(Roles ="Manager")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                throw new DataNotFoundException();
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

        [Authorize(Roles ="Manager")]
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
        [Authorize(Roles ="Manager")]
        public ActionResult Create()
        {
            return View(InitCustomerEditViewModel());
        }

        // POST: Customers/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Manager")]
        public ActionResult Create(CustomerEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //密碼加密
                string salt = Guid.NewGuid().ToString();
                byte[] pwdAndSalt = Encoding.UTF8.GetBytes(vm.Customer.Password + salt);
                byte[] hashBytes = new SHA256Managed().ComputeHash(pwdAndSalt);
                string hash = Convert.ToBase64String(hashBytes);
                vm.Customer.Password = hash;

                客戶資料 newData = vm.Customer;
                CustomerRepo.Add(newData);
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(vm);
        }

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        [Authorize(Roles ="Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                throw new DataNotFoundException();
            }

            return View(InitCustomerEditViewModel(客戶資料));
        }

        // POST: Customers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Manager")]
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

        [HandleError(ExceptionType = typeof(DataNotFoundException), View = "Error")]
        [HandleError(ExceptionType = typeof(NoPrimaryKeyPassException), View = "Error")]
        [Authorize(Roles ="Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                throw new NoPrimaryKeyPassException();
            }
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id.Value);
            if (客戶資料 == null)
            {
                throw new DataNotFoundException();
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
        [Authorize(Roles ="Manager")]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = CustomerRepo.GetCustomerById(id);
            CustomerRepo.Delete(客戶資料);
            CustomerRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles ="Manager")]
        public JsonResult GetCustomerAmount()
        {
            return Json(new {
                Count=CustomerRepo.GetCustomerAmount()
            }, JsonRequestBehavior.AllowGet);
        }


        /// <summary> 取得客戶底下所有聯絡人 </summary>
        /// <param name="id"></param>
        /// <returns>部分檢視</returns>
        [Authorize(Roles ="Manager")]
        public ActionResult ListAllContacts(int id)
        {
            if (id == 0)
                return new HttpNotFoundResult("請輸入客戶ID");

            var list = this.ContactsRepo.GetListByCustomerId(id);
            return PartialView("_ContactList", list);
        }

        [Authorize(Roles = "Customer")]
        public ActionResult CustomerEdit()
        {
            //取得登入user的帳戶
            var customerAccount = User.Identity.Name;
            var customerData = this.CustomerRepo.All().Where(x => x.Account == customerAccount).Select(x=>new CustomerOnlyEditViewModel() {
                Email=x.Email,
                Id=x.Id,
                傳真=x.傳真,
                地址=x.地址,
                客戶名稱=x.客戶名稱,
                客戶類別=x.客戶類別.類別名稱,
                統一編號=x.統一編號,
                電話=x.電話
            }).FirstOrDefault();
            if (customerData != null)
            {
                return View(customerData);
            }
            return RedirectToAction("Login", "Home");
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public ActionResult CustomerEdit(CustomerOnlyEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                CustomerRepo.UpdateCustomerOnlyData(vm);
                CustomerRepo.UnitOfWork.Commit();
                return RedirectToAction("CustomerEdit");
            }
            return View(vm);
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
