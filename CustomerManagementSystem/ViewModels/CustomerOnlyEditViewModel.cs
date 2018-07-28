using CustomerManagementSystem.Models.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class CustomerOnlyEditViewModel:IValidatableObject
    {
        [Required]
        public int Id { get; set; }

        public string 客戶名稱 { get; set; }

        public string 統一編號 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 電話 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 傳真 { get; set; }

        [StringLength(100, ErrorMessage = "欄位長度不得大於 100 個字元")]
        public string 地址 { get; set; }

        [StringLength(250, ErrorMessage = "欄位長度不得大於 250 個字元")]
        [EmailAddress]
        public string Email { get; set; }

        public string 客戶類別 { get; set; }

        [DisplayName("新密碼(填入則修改密碼)")]
        public string NewPassword { get; set; }
        [DisplayName("新密碼確認")]
        public string NewPasswordConfirm { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(NewPassword) && !string.IsNullOrEmpty(NewPasswordConfirm) && !NewPassword.Equals(NewPasswordConfirm))
            {
                yield return new ValidationResult(
                "請填入與新密碼同樣的內容", new[] { "NewPasswordConfirm" });
            }
        }
    }
}