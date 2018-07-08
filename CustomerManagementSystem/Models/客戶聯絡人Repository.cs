using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagementSystem.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
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
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{
        客戶聯絡人 GetContactById(int id);

    }
}