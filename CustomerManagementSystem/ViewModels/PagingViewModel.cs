using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class PagingViewModel
    {
        public int Count { get; set; }

        public int Skip { get; set; }

        public int Take { get; set; }

        public PagingViewModel()
        {
            this.Skip = 0;
            this.Take = 10;
        }
    }
}