using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagementSystem.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
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
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{
        客戶銀行資訊 GetBankAccountById(int id);

    }
}