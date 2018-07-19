using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace CustomerManagementSystem.Helpers
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// 產生排序用的超連結
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression">VM中用來排序的屬性</param>
        /// <param name="linkText">超連結文字</param>
        /// <param name="actionName">Action Name</param>
        /// <param name="routeValuesNow">目前的route value dictionary</param>
        /// <param name="sortColumnName">要排序的欄位名稱</param>
        /// <param name="defaultNextSort">預設的第一次按下去的排序方向 asc desc</param>
        /// <returns></returns>
        public static MvcHtmlString SortActionLink<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string linkText, string actionName, RouteValueDictionary routeValuesNow,string sortColumnName,string defaultNextSort)
        {
            //新建一個route dictionary
            RouteValueDictionary routeValuesNew = new RouteValueDictionary(routeValuesNow);

            //取得排序屬性的名稱
            string sortPropName = ((MemberExpression)expression.Body).Member.Name;
            //是否改變排序的欄位
            bool changeSortColumn = false;
            //排序條件在route裡面的binding key
            string columnKey = sortPropName + ".Column";
            string sortKey = sortPropName + ".Sort";
            //若有升降序則加入ICON
            string iconClassName = string.Empty;

            //欄位名稱設定
            if (routeValuesNew[columnKey] == null)
            {
                routeValuesNew.Add(columnKey, sortColumnName);
            }
            else
            {
                //判斷是否換了排序的欄位
                changeSortColumn = sortColumnName != routeValuesNew[columnKey].ToString();
                routeValuesNew[columnKey] = sortColumnName;
            }
            
            //排序方向設定
            if (routeValuesNew[sortKey] == null)
            {
                //若原本無排序 則用預設下一個排序
                routeValuesNew.Add(sortKey, defaultNextSort);
            }
            else
            {
                //若有欄位 且換了欄位，則排序重來，從指定的排序開始
                if (changeSortColumn)
                {
                    routeValuesNew[sortKey] = defaultNextSort;
                }
                else
                {
                    //若沒換欄位，則排序往下一順位(不指定→asc→desc→不指定)
                    switch (routeValuesNew[sortKey].ToString())
                    {
                        case ("asc"):
                            routeValuesNew[sortKey] = "desc";//下一次按時會換的方向
                            iconClassName = "glyphicon glyphicon-arrow-up";//目前要用的ICON
                            break;
                        case ("desc"):
                            //不能指定空的，用移除這個KEY VALUE的方式變為不指定
                            routeValuesNew.Remove(sortKey);//下一次按時會換的方向
                            iconClassName = "glyphicon glyphicon-arrow-down";//目前要用的ICON
                            break;
                        case (""):
                            routeValuesNew[sortKey] = "asc";//下一次按時會換的方向
                            iconClassName = string.Empty;//目前要用的ICON
                            break;
                        default:
                            break;
                    }
                }

            }
            var baseLink= htmlHelper.ActionLink(linkText
               , actionName
               , routeValuesNew);
            if (!string.IsNullOrEmpty(iconClassName))
            {
                var icon = string.Format($"<span class=\"{iconClassName}\" aria-hidden=\"true\"></span>");
                return new MvcHtmlString(baseLink.ToHtmlString().Insert(baseLink.ToHtmlString().IndexOf(@"</a>"), icon));
            }
            else
            {
                return baseLink;
            }

        }
    }
}