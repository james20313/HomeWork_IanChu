using System.ComponentModel.DataAnnotations;

[MetadataType(typeof(客戶類別MetaData))]
public partial class 客戶類別
{
}

public partial class 客戶類別MetaData
{
    [Required]
    public int Id { get; set; }
    
    [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
    [Required]
    public string 類別名稱 { get; set; }

    public virtual ICollection<客戶資料> 客戶資料 { get; set; }
}
