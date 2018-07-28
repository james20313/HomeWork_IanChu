using CustomerManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomersQueryViewModel
    {
        public IPagedList<CustomersViewModel> Customers { get; set; }

        public List<SelectListItem> CustomerTypeList { get; set; }

        public CustomerQueryInModel Query { get; set; }

        public PagingViewModel Paging { get; set; }

        public SortingViewModel Sort { get; set; }

        public CustomersQueryViewModel()
        {
            this.Paging = new PagingViewModel();
            this.Query = new CustomerQueryInModel();
            this.Sort = new SortingViewModel();
        }
    }
}