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

        public int Count { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public CustomersQueryViewModel()
        {
            this.Skip = 0;
            this.Take = 10;
            this.Query = new CustomerQueryInModel();
        }
    }
}