using System;
using System.Linq;
using System.Collections.Generic;
using CustomerManagementSystem.ViewModels;

namespace CustomerManagementSystem.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public 客戶資料 GetCustomerById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id==id);
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
            }
            return query;
        }

        public List<客戶資料> Search(CustomerQueryInModel cond,PagingViewModel paging)
        {
            var query = this.GetSearchIQueryable(cond);
            return query.OrderByDescending(x=>x.Id).Skip(paging.Skip).Take(paging.Take).ToList();
        }

        public int SearchCount(CustomerQueryInModel cond)
        {
            var query = this.GetSearchIQueryable(cond);
            return query.Count();
        }

        public List<CustomerReportViewModel> GetReport()
        {
            return this.All().OrderBy(x=>x.Id).Select(x => new CustomerReportViewModel() {
                CustomerName=x.客戶名稱,
                BankAccountAmount=x.客戶銀行資訊.Count(),
                ContactAmount=x.客戶聯絡人.Count()
            }).ToList();
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        客戶資料 GetCustomerById(int id);
        IQueryable<客戶資料> GetSearchIQueryable(CustomerQueryInModel cond);
        List<客戶資料> Search(CustomerQueryInModel cond, PagingViewModel paging);
        int SearchCount(CustomerQueryInModel cond);
        List<CustomerReportViewModel> GetReport();
    }
}