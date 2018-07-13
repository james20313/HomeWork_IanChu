using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerContactQueryCondition
    {
        [DisplayName("客戶名稱")]
        public string CustomerName { get; set; }

        [DisplayName("公司統編")]
        public string CompanyNumber { get; set; }

        [DisplayName("職稱")]
        public string JobName { get; set; }

        [DisplayName("姓名")]
        public string ContactName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("手機")]
        public string Mobile { get; set; }

        [DisplayName("電話")]
        public string Phone { get; set; }
    }
}