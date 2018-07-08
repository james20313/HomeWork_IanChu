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
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{
        客戶資料 GetCustomerById(int id);

    }
}