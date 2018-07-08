using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CustomerManagementSystem.Models.Attribute
{
    /// <summary>
    /// 手機號碼驗證屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class CellphoneNumberAttribute: DataTypeAttribute
    {
        public CellphoneNumberAttribute() : base(DataType.Text)
        {
            ErrorMessage = "請輸入正確手機格式 09xx-xxxxxx";
        }

        public override bool IsValid(object value)
        {
            string str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return CheckCellphone(str);
            else
                return true;//空的就不檢查
        }

        private bool CheckCellphone(string str)
        {
            Regex rgx = new Regex(@"\d{4}-\d{6}");
            return rgx.IsMatch(str);
        }
    }
}