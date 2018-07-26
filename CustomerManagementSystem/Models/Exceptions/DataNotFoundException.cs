using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models.Exceptions
{

    [Serializable]
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException():base("找不到此筆資料，或此資料已經被刪除!") { }
        public DataNotFoundException(string message) : base(message) { }
        public DataNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected DataNotFoundException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}