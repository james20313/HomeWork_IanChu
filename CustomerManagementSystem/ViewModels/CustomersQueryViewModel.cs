using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomersQueryViewModel
    {
        public List<CustomersViewModel> Customers { get; set; }

        public CustomerQueryInModel Query { get; set; }

        public PagingViewModel Paging { get; set; }

        public CustomersQueryViewModel()
        {
            this.Customers = new List<CustomersViewModel>();
            this.Paging = new PagingViewModel();
            this.Query = new CustomerQueryInModel();
        }
    }
}