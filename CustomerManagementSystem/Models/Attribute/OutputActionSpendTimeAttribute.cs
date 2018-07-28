using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagementSystem.Models.Attribute
{
    public class OutputActionSpendTimeAttribute : ActionFilterAttribute
    {
        private void Log(string text)
        {
            System.Diagnostics.Debug.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] {text}");
        }
        Stopwatch stopWatchForAction;
        Stopwatch stopWatchForActionResult;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatchForAction = new Stopwatch();
            stopWatchForAction.Start();
            Log($"Action [{filterContext.ActionDescriptor.ActionName}] 執行中...");
            base.OnActionExecuting(filterContext);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            stopWatchForAction.Stop();
            Log($"Action [{filterContext.ActionDescriptor.ActionName}] 結束執行，共花費{stopWatchForAction.Elapsed.TotalSeconds}秒");
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            stopWatchForActionResult = new Stopwatch();
            stopWatchForActionResult.Start();
            Log($"Action Result 執行中...");
            base.OnResultExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            stopWatchForActionResult.Stop();
            Log($"Action Result 結束執行，共花費{stopWatchForActionResult.Elapsed.TotalSeconds}秒");
            base.OnResultExecuted(filterContext);
        }
    }
}