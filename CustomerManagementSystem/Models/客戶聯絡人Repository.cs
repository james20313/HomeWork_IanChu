using System;
using System.Linq;
using System.Collections.Generic;
using CustomerManagementSystem.ViewModels;
using System.Data.Entity;

namespace CustomerManagementSystem.Models
{   
	public class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public 客戶聯絡人 GetContactById(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x=>x.IsDeleted==false);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDeleted = true;
        }

        public IQueryable<客戶聯絡人> GetSearchIQuerable(CustomerContactQueryCondition cond)
        {
            var query = this.All();
            if (cond != null)
            {
                query = query.Where(x => (string.IsNullOrEmpty(cond.CompanyNumber) || x.客戶資料.統一編號.Equals(cond.CompanyNumber))
                && (string.IsNullOrEmpty(cond.CustomerName) || x.客戶資料.客戶名稱.Contains(cond.CustomerName))
                && (string.IsNullOrEmpty(cond.ContactName) || x.姓名.Contains(cond.ContactName))
                && (string.IsNullOrEmpty(cond.Email) || x.Email.Equals(cond.Email))
                && (string.IsNullOrEmpty(cond.JobName) || x.職稱.Equals(cond.JobName))
                && (string.IsNullOrEmpty(cond.Mobile) || x.手機.Equals(cond.Mobile))
                && (string.IsNullOrEmpty(cond.Phone) || x.電話.Equals(cond.Phone)));
            }
            return query;
        }

        public List<CustomerContactViewModel> Search(CustomerContactQueryCondition cond,PagingViewModel paging,SortingViewModel sort)
        {
            return this.GetSearchIQuerable(cond)
                .Sort(sort)
                .Skip(paging.Skip)
                .Take(paging.Take)
                .AsNoTracking()
                .Select(x=>new CustomerContactViewModel() {
                CompanyNumber=x.客戶資料.統一編號,
                CustomerName=x.客戶資料.客戶名稱,
                Email=x.Email,
                Id=x.Id,
                姓名=x.姓名,
                手機=x.手機,
                職稱=x.職稱,
                電話=x.電話
            }).ToList();
        }

        public List<CustomerContactViewModel> SearchAll(CustomerContactQueryCondition cond)
        {
            return this.GetSearchIQuerable(cond).OrderByDescending(x => x.Id).AsNoTracking().Select(x => new CustomerContactViewModel()
            {
                CompanyNumber = x.客戶資料.統一編號,
                CustomerName = x.客戶資料.客戶名稱,
                Email = x.Email,
                Id = x.Id,
                姓名 = x.姓名,
                手機 = x.手機,
                職稱 = x.職稱,
                電話 = x.電話
            }).ToList();
        }

        public int SearchCount(CustomerContactQueryCondition cond)
        {
            return this.GetSearchIQuerable(cond).AsNoTracking().Count();
        }

        public bool IsEmailExist(string email,int customerId)
        {
            return this.All().Any(x => x.客戶Id == customerId && x.Email.Equals(email));
        }

        /// <summary> 取得客戶底下所有聯絡人 </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<CustomerContactViewModel> GetListByCustomerId(int id)
        {
            return this.All().Where(x => x.客戶Id == id).Select(x => new CustomerContactViewModel
            {
                CompanyNumber=x.客戶資料.統一編號,
                CustomerName=x.客戶資料.客戶名稱,
                Id=x.Id,
                Email=x.Email,
                姓名=x.姓名,
                手機=x.手機,
                職稱=x.職稱,
                電話 =x.電話
            }).ToList();
        }

        public void BatchUpdate(List<CustomerContactViewModel> list)
        {
            foreach (var item in list)
            {
                var existData = this.GetContactById(item.Id);
                if (existData == null)
                {
                    throw new Exception("找不到此客戶");
                }
                existData.職稱 = item.職稱;
                existData.手機 = item.手機;
                existData.電話 = item.電話;
            }
        }
    }

	public interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{
        客戶聯絡人 GetContactById(int id);
        IQueryable<客戶聯絡人> GetSearchIQuerable(CustomerContactQueryCondition cond);
        List<CustomerContactViewModel> Search(CustomerContactQueryCondition cond, PagingViewModel paging, SortingViewModel sort);
        List<CustomerContactViewModel> SearchAll(CustomerContactQueryCondition cond);
        int SearchCount(CustomerContactQueryCondition cond);
        bool IsEmailExist(string email, int customerId);
        List<CustomerContactViewModel> GetListByCustomerId(int id);
        void BatchUpdate(List<CustomerContactViewModel> list);
    }
}