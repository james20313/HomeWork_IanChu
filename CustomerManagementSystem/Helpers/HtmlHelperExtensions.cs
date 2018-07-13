using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CustomerManagementSystem.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes, bool disabled)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (disabled)
            {
                attrs.Add("disabled", "disabled");
            }

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attrs);
        }

        public static MvcHtmlString DropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, SelectList selectList, string optionLabel, object htmlAttributes, bool disabled)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (disabled)
            {
                attrs.Add("disabled", "disabled");
            }

            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attrs);
        }
    }
}