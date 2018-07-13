using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomersQueryViewModel
    {
        public List<CustomersViewModel> Customers { get; set; }

        public List<SelectListItem> CustomerTypeList { get; set; }

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