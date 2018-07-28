using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomersViewModel
    {
        public int Id { get; set; }

        [DisplayName("客戶名稱")]
        public string ClientName { get; set; }

        [DisplayName("統一編號")]
        public string CompanyNumber { get; set; }

        [DisplayName("客戶類別")]
        public string CustomerTypeName { get; set; }

        [DisplayName("電話")]
        [UIHint("PhoneNumber")]
        public string Phone { get; set; }

        [DisplayName("傳真")]
        public string Fax { get; set; }

        [DisplayName("地址")]
        [UIHint("GoogleMapLinkAddress")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [UIHint("EmailAddress")]
        public string Email { get; set; }
    }
}