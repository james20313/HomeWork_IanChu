using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models.Attribute
{
    /// <summary>
    /// 台灣公司統一編號驗證屬性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class TaiwanCompanyNumberAttribute :DataTypeAttribute
    {
        public TaiwanCompanyNumberAttribute():base(DataType.Text)
        {
            ErrorMessage = "請輸入正確統一編號格式";
        }

        public override bool IsValid(object value)
        {
            string str = (string)value;
            if (!string.IsNullOrEmpty(str))
                return CheckCompanyN(str);
            else
                return true;//空的就不檢查
        }
        /// <summary>
        /// REF:http://demo.tc/post/382
        /// </summary>
        /// <param name="CompanyId"></param>
        /// <returns></returns>
        private Boolean CheckCompanyN(string CompanyId)
        {
            int CompanyNo;
            if (CompanyId == null || CompanyId.Trim().Length != 8)
                return false;
            if (!int.TryParse(CompanyId, out CompanyNo))
                return false;
            int[] Logic = new int[] { 1, 2, 1, 2, 1, 2, 4, 1 };
            int addition, sum = 0, j = 0, x;
            for (x = 0; x < Logic.Length; x++)
            {
                int no = Convert.ToInt32(CompanyId.Substring(x, 1));
                j = no * Logic[x];
                addition = ((j / 10) + (j % 10));
                sum += addition;
            }
            if (sum % 10 == 0)
            {
                return true;
            }
            if (CompanyId.Substring(6, 1) == "7")
            {
                if (sum % 10 == 9)
                {
                    return true;
                }
            }
            return false;
        }
    }
}