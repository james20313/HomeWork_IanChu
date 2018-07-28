using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerContactsQueryViewModel
    {
        public List<CustomerContactViewModel> Contacts { get; set; }

        public CustomerContactQueryCondition Query { get; set; }

        public PagingViewModel Paging { get; set; }

        public SortingViewModel Sort { get; set; }

        public bool BatchEdit { get; set; }

        public CustomerContactsQueryViewModel()
        {
            this.Contacts = new List<CustomerContactViewModel>();
            this.Query = new CustomerContactQueryCondition();
            this.Paging = new PagingViewModel();
            this.Sort = new SortingViewModel();
        }
    }
}