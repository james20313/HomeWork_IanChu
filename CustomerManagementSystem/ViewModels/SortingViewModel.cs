using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class SortingViewModel
    {
        /// <summary> 欄位名稱 </summary>
        public string Column { get; set; }

        /// <summary> 排序方向 asc / desc </summary>
        public string Sort { get; set; }
    }
}