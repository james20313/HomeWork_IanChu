using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels.Out
{
    public class CustomersViewModel
    {
        public string Id { get; set; }

        [DisplayName("客戶名稱")]
        public string ClientName { get; set; }

        [DisplayName("統一編號")]
        public string CompanyNumber { get; set; }

        [DisplayName("電話")]
        public string Phone { get; set; }

        [DisplayName("傳真")]
        public string Fax { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }
    }
}