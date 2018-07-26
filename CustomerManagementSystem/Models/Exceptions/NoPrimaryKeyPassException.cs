using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models.Exceptions
{

    [Serializable]
    public class NoPrimaryKeyPassException : Exception
    {
        public NoPrimaryKeyPassException():base("沒有主鍵值傳入，不知道要存取哪筆資料!") { }
        public NoPrimaryKeyPassException(string message) : base(message) { }
        public NoPrimaryKeyPassException(string message, Exception inner) : base(message, inner) { }
        protected NoPrimaryKeyPassException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}