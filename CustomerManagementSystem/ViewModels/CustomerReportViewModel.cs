using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerReportViewModel
    {
        [DisplayName("客戶姓名")]
        public string CustomerName { get; set; }

        [DisplayName("聯絡人數量")]
        public int ContactAmount { get; set; }

        [DisplayName("銀行帳戶數量")]
        public int BankAccountAmount { get; set; }
    }
}