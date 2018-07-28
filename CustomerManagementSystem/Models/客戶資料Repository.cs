using System;
using System.Linq;
using System.Collections.Generic;
using CustomerManagementSystem.ViewModels;
using System.Data.Entity;

namespace CustomerManagementSystem.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public 客戶資料 GetCustomerById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(x => x.IsDeleted == false);
        }

        public override void Delete(客戶資料 entity)
        {
            entity.IsDeleted = true;
        }

        public IQueryable<客戶資料> GetSearchIQueryable(CustomerQueryInModel cond)
        {
            var query = this.All();
            if (cond != null)
            {
                if (!string.IsNullOrEmpty(cond.Address))
                {
                    query = query.Where(x => x.地址.Contains(cond.Address));
                }
                if (!string.IsNullOrEmpty(cond.ClientName))
                {
                    query = query.Where(x => x.客戶名稱.Contains(cond.ClientName));
                }
                if (!string.IsNullOrEmpty(cond.CompanyNumber))
                {
                    query = query.Where(x => x.統一編號.Equals(cond.CompanyNumber));
                }
                if (!string.IsNullOrEmpty(cond.Email))
                {
                    query = query.Where(x => x.Email.Equals(cond.Email));
                }
                if (!string.IsNullOrEmpty(cond.Fax))
                {
                    query = query.Where(x => x.傳真.Equals(cond.Fax));
                }
                if (!string.IsNullOrEmpty(cond.Phone))
                {
                    query = query.Where(x => x.電話.Equals(cond.Phone));
                }
                if (cond.CustomerTypeId.HasValue)
                {
                    query = query.Where(x => x.類別Id == cond.CustomerTypeId.Value);
                }
            }
            return query;
        }

        public List<客戶資料> Search(CustomerQueryInModel cond, PagingViewModel paging, SortingViewModel sort)
        {
            var query = this.GetSearchIQueryable(cond);
            return query.Sort(sort).Skip(paging.Skip).Take(paging.Take).AsNoTracking().ToList();
        }

        public List<客戶資料> SearchAll(CustomerQueryInModel cond)
        {
            var query = this.GetSearchIQueryable(cond);
            return query.OrderByDescending(x => x.Id).AsNoTracking().ToList();
        }

        public int SearchCount(CustomerQueryInModel cond)
        {
            var query = this.GetSearchIQueryable(cond);
            return query.AsNoTracking().Count();
        }

        public List<CustomerReportViewModel> GetReport()
        {
            return this.All().OrderBy(x => x.Id).AsNoTracking().Select(x => new CustomerReportViewModel()
            {
                CustomerName = x.客戶名稱,
                BankAccountAmount = x.客戶銀行資訊.Count(),
                ContactAmount = x.客戶聯絡人.Count()
            }).ToList();
        }

        public int GetCustomerAmount()
        {
            return this.All().Count();
        }

        public void Update(客戶資料 data)
        {
            var existData = this.GetCustomerById(data.Id);
            existData.Email = data.Email;
            existData.Account = data.Account;
            existData.傳真 = data.傳真;
            existData.地址 = data.地址;
            existData.客戶名稱 = data.客戶名稱;
            existData.統一編號 = data.統一編號;
            existData.電話 = data.電話;
            existData.類別Id = data.類別Id;
        }

        public void UpdateCustomerOnlyData(CustomerOnlyEditViewModel vm)
        {
            var existData = this.All().Where(x=>x.Id==vm.Id).FirstOrDefault();
            if (existData == null)
            {
                throw new Exception("找不到此客戶");
            }
            existData.電話 = vm.電話;
            existData.傳真 = vm.傳真;
            existData.地址 = vm.地址;
            existData.Email = vm.Email;
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {
        客戶資料 GetCustomerById(int id);
        IQueryable<客戶資料> GetSearchIQueryable(CustomerQueryInModel cond);
        List<客戶資料> Search(CustomerQueryInModel cond, PagingViewModel paging, SortingViewModel sort);
        List<客戶資料> SearchAll(CustomerQueryInModel cond);
        int SearchCount(CustomerQueryInModel cond);
        List<CustomerReportViewModel> GetReport();
        int GetCustomerAmount();
        void UpdateCustomerOnlyData(CustomerOnlyEditViewModel vm);
    }
}