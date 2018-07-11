using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels.In
{
    public class CustomerQueryInModel
    {
        public string ClientName { get; set; }

        public string CompanyNumber { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }
    }
}