using System;
using System.Linq;
using System.Collections.Generic;
using CustomerManagementSystem.ViewModels;
using System.Data.Entity;

namespace CustomerManagementSystem.Models
{   
	public class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 GetBankAccountById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(x=>x.IsDeleted==false);
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.IsDeleted = true;
        }

        public IQueryable<客戶銀行資訊> GetSearchIQuerable(BankAccountQueryCondition cond)
        {
            var query = this.All();
            if (cond != null)
            {
                query=query.Where(x=>(string.IsNullOrEmpty(cond.AccountName) || x.帳戶名稱.Equals(cond.AccountName))
                && (string.IsNullOrEmpty(cond.AccountNumber) || x.帳戶號碼.Equals(cond.AccountNumber))
                && (!cond.BankCode.HasValue || x.銀行代碼 == cond.BankCode.Value)
                && (!cond.BankSubCode.HasValue || x.分行代碼 == cond.BankSubCode.Value)
                && (string.IsNullOrEmpty(cond.BankName) || x.銀行名稱.Contains(cond.BankName))
                && (string.IsNullOrEmpty(cond.CompanyNumber) || x.客戶資料.統一編號.Equals(cond.CompanyNumber))
                && (string.IsNullOrEmpty(cond.CustomerName) || x.客戶資料.客戶名稱.Contains(cond.CustomerName))
                );
            }
            return query;
        }

        public List<BankAccountViewModel> Search(BankAccountQueryCondition cond,PagingViewModel paging)
        {
            return this.GetSearchIQuerable(cond).OrderByDescending(x => x.Id).Skip(paging.Skip).Take(paging.Take).AsNoTracking().Select(x => new BankAccountViewModel() {
                AccountName=x.帳戶名稱,
                AccountNumber=x.帳戶號碼,
                BankCode=x.銀行代碼,
                BankName=x.銀行名稱,
                BankSubCode=x.分行代碼,
                CompanyNumber=x.客戶資料.統一編號,
                CustomerName=x.客戶資料.客戶名稱,
                Id=x.Id
            }).ToList();
        }

        public List<BankAccountViewModel> Search(BankAccountQueryCondition cond)
        {
            return this.GetSearchIQuerable(cond).OrderByDescending(x => x.Id).AsNoTracking().Select(x => new BankAccountViewModel()
            {
                AccountName = x.帳戶名稱,
                AccountNumber = x.帳戶號碼,
                BankCode = x.銀行代碼,
                BankName = x.銀行名稱,
                BankSubCode = x.分行代碼,
                CompanyNumber = x.客戶資料.統一編號,
                CustomerName = x.客戶資料.客戶名稱,
                Id = x.Id
            }).ToList();
        }

        public int SearchCount(BankAccountQueryCondition cond)
        {
            return this.GetSearchIQuerable(cond).OrderByDescending(x => x.Id).AsNoTracking().Count();
        }
    }

	public interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
        客戶銀行資訊 GetBankAccountById(int id);
        IQueryable<客戶銀行資訊> GetSearchIQuerable(BankAccountQueryCondition cond);
        List<BankAccountViewModel> Search(BankAccountQueryCondition cond);
        List<BankAccountViewModel> Search(BankAccountQueryCondition cond, PagingViewModel paging);
        int SearchCount(BankAccountQueryCondition cond);
    }
}