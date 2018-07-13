using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class BankAccountViewModel
    {
        public int Id { get; set; }

        [DisplayName("公司統編")]
        public string CompanyNumber { get; set; }

        [DisplayName("客戶姓名")]
        public string CustomerName { get; set; }

        [DisplayName("銀行名稱")]
        public string BankName { get; set; }

        [DisplayName("銀行代碼")]
        public int BankCode { get; set; }

        [DisplayName("分行代碼")]
        public Nullable<int> BankSubCode { get; set; }

        [DisplayName("帳戶名稱")]
        public string AccountName { get; set; }

        [DisplayName("帳戶號碼")]
        public string AccountNumber { get; set; }
    }
}