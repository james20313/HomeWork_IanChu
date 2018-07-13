namespace CustomerManagementSystem.Models
{
    using Attribute;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(客戶類別MetaData))]
    public partial class 客戶類別
    {
    }

    public partial class 客戶類別MetaData
    {
        [Required]
        public CustomerTypeEnum Id { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 類別名稱 { get; set; }

        public virtual ICollection<客戶資料> 客戶資料 { get; set; }
    }

    public enum CustomerTypeEnum
    {
        一般客戶 = 1,
        月結客戶 = 2,
        VIP客戶 = 3,
        到付應收客戶 = 4,
    }
}