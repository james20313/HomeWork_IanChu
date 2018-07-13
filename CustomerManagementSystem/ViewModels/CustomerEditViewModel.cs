using CustomerManagementSystem.Models;
using CustomerManagementSystem.Models.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerEditViewModel
    {
        public 客戶資料 Customer { get; set; }

        public List<SelectListItem> CustomerTypeList { get; set; }
    }
}