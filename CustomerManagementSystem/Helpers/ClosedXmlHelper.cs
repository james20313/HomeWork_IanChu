using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebApplication1.Models
{
    public static class ClosedXmlHelper
    {
        public static XLWorkbook ToClosedXmlExcel<TModel>(this List<TModel> list)
        {
            var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("工作表1");

            int rowNow = 1;
            int colNow = 1;

            if (list != null && list.Any())
            {
                //迴圈印出資料類別屬性的顯示名稱
                object dataCol = list[0];
                Type dataColType = dataCol.GetType();
                PropertyInfo[] dataColProperties = dataColType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in dataColProperties)
                {
                    var hasAttr = Attribute.IsDefined(prop, typeof(DisplayNameAttribute));
                    string displayName = GetDisplayName(dataColType, prop, hasAttr);
                    ws.Cell(rowNow, colNow).Value = displayName;
                    colNow++;
                }
                rowNow++;
                colNow = 1;

                //迴圈印出每個值
                foreach (var data in list)
                {
                    Type modelType = data.GetType();
                    PropertyInfo[] properties = modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var prop in properties)
                    {
                        if (prop.PropertyType == typeof(string))
                            ws.Cell(rowNow, colNow).Value = "'" + prop.GetValue(data);
                        else
                            ws.Cell(rowNow, colNow).Value = prop.GetValue(data);
                        colNow++;
                    }
                    rowNow++;
                    colNow = 1;
                }
            }
            return wb;
        }
        private static String GetDisplayName(Type type, PropertyInfo info, bool hasMetaDataAttribute)
        {
            if (!hasMetaDataAttribute)
            {
                object[] attributes = info.GetCustomAttributes(typeof(DisplayNameAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    var displayName = (DisplayNameAttribute)attributes[0];
                    return displayName.DisplayName;
                }
                return info.Name;
            }
            PropertyDescriptor propDesc = TypeDescriptor.GetProperties(type).Find(info.Name, true);
            DisplayNameAttribute displayAttribute =
                propDesc.Attributes.OfType<DisplayNameAttribute>().FirstOrDefault();
            return displayAttribute != null ? displayAttribute.DisplayName : null;
        }
    }
}