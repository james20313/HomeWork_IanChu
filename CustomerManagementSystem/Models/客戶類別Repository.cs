using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagementSystem.Models
{   
	public  class 客戶類別Repository : EFRepository<客戶類別>, I客戶類別Repository
	{

	}

	public  interface I客戶類別Repository : IRepository<客戶類別>
	{

	}
}