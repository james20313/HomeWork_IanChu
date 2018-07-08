using System;
using System.Linq;
using System.Collections.Generic;
	
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
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        客戶資料 GetCustomerById(int id);

    }
}