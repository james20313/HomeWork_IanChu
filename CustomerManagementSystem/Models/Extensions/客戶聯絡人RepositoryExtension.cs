using CustomerManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models
{
    public static class 客戶聯絡人RepositoryExtension
    {
        public static IQueryable<客戶聯絡人> Sort(this IQueryable<客戶聯絡人> baseQuery, SortingViewModel sort)
        {
            if (!string.IsNullOrEmpty(sort.Sort) && !string.IsNullOrEmpty(sort.Column))
            {
                if (sort.Sort.Equals("asc"))
                {
                    switch (sort.Column)
                    {
                        case ("CompanyNumber"):
                            baseQuery = baseQuery.OrderBy(x => x.客戶資料.統一編號);
                            break;
                        case ("CustomerName"):
                            baseQuery = baseQuery.OrderBy(x => x.客戶資料.客戶名稱);
                            break;
                        case ("ContactName"):
                            baseQuery = baseQuery.OrderBy(x => x.姓名);
                            break;
                        case ("Mobile"):
                            baseQuery = baseQuery.OrderBy(x => x.手機);
                            break;
                        case ("Phone"):
                            baseQuery = baseQuery.OrderBy(x => x.電話);
                            break;
                        case ("JobName"):
                            baseQuery = baseQuery.OrderBy(x => x.職稱);
                            break;
                        case ("Email"):
                            baseQuery = baseQuery.OrderBy(x => x.Email);
                            break;
                        case ("Id"):
                            baseQuery = baseQuery.OrderBy(x => x.Id);
                            break;
                        default:
                            break;
                    }
                }
                else if (sort.Sort.Equals("desc"))
                {
                    switch (sort.Column)
                    {
                        case ("CompanyNumber"):
                            baseQuery = baseQuery.OrderByDescending(x => x.客戶資料.統一編號);
                            break;
                        case ("CustomerName"):
                            baseQuery = baseQuery.OrderByDescending(x => x.客戶資料.客戶名稱);
                            break;
                        case ("ContactName"):
                            baseQuery = baseQuery.OrderByDescending(x => x.姓名);
                            break;
                        case ("Mobile"):
                            baseQuery = baseQuery.OrderByDescending(x => x.手機);
                            break;
                        case ("Phone"):
                            baseQuery = baseQuery.OrderByDescending(x => x.電話);
                            break;
                        case ("JobName"):
                            baseQuery = baseQuery.OrderByDescending(x => x.職稱);
                            break;
                        case ("Email"):
                            baseQuery = baseQuery.OrderByDescending(x => x.Email);
                            break;
                        case ("Id"):
                            baseQuery = baseQuery.OrderByDescending(x => x.Id);
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                baseQuery = baseQuery.OrderByDescending(x => x.Id);
            }
            return baseQuery;
        }
    }
}