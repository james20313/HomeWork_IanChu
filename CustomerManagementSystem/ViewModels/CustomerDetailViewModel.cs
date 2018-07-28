using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerDetailViewModel
    {
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }

        [UIHint("PhoneNumber")]
        public string 電話 { get; set; }
        public string 傳真 { get; set; }

        [UIHint("GoogleMapLinkAddress")]
        public string 地址 { get; set; }

        [UIHint("EmailAddress")]
        public string Email { get; set; }

        [DisplayName("客戶類別")]
        public string CustomerTypeName { get; set; }
    }
}