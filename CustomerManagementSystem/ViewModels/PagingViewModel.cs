using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.ViewModels
{
    public class PagingViewModel
    {
        private int _Take = 10;

        public int Take
        {
            get { return this._Take; }
            set { this._Take = value; }
        }
        public int Count { get; set; }

        public int Skip { get {
                //第一頁的話就不用跳過,第二頁的話就跳過第一頁的每頁筆數
                if (Page == 0)
                    return 0;
                else
                    return (Page-1) * Take;
            } }

        public int Page { get; set; }

        public PagingViewModel()
        {
            this.Page = 0;
        }
    }
}