using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class BankAccountQueryViewModel
    {
        public List<BankAccountViewModel> BankAccounts { get; set; }

        public BankAccountQueryCondition Query { get; set; }

        public PagingViewModel Paging { get; set; }

        public BankAccountQueryViewModel()
        {
            this.BankAccounts = new List<BankAccountViewModel>();
            this.Query = new BankAccountQueryCondition();
            this.Paging = new PagingViewModel();
        }
    }
}