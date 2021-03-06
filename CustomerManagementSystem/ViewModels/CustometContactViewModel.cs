﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerContactViewModel
    {
        public int Id { get; set; }

        [DisplayName("客戶名稱")]
        public string CustomerName { get; set; }

        [DisplayName("公司統編")]
        public string CompanyNumber { get; set; }

        [DisplayName("職稱")]
        public string 職稱 { get; set; }

        [DisplayName("姓名")]
        public string 姓名 { get; set; }

        [DisplayName("Email")]
        [UIHint("EmailAddress")]
        public string Email { get; set; }

        [DisplayName("手機")]
        [UIHint("PhoneNumber")]
        public string 手機 { get; set; }

        [DisplayName("電話")]
        [UIHint("PhoneNumber")]
        public string 電話 { get; set; }
    }
}