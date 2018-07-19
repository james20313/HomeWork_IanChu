using CustomerManagementSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagementSystem.Models
{
    public static class 客戶資料RepositoryExtension
    {
        public static IQueryable<客戶資料> Sort(this IQueryable<客戶資料> baseQuery, SortingViewModel sort)
        {
            if (!string.IsNullOrEmpty(sort.Sort) && !string.IsNullOrEmpty(sort.Column))
            {
                if (sort.Sort.Equals("asc"))
                {
                    switch (sort.Column)
                    {
                        case ("ClientName"):
                            baseQuery = baseQuery.OrderBy(x => x.客戶名稱);
                            break;
                        case ("CompanyNumber"):
                            baseQuery = baseQuery.OrderBy(x => x.統一編號);
                            break;
                        case ("CustomerTypeName"):
                            baseQuery = baseQuery.OrderBy(x => x.客戶類別.類別名稱);
                            break;
                        case ("Phone"):
                            baseQuery = baseQuery.OrderBy(x => x.電話);
                            break;
                        case ("Fax"):
                            baseQuery = baseQuery.OrderBy(x => x.傳真);
                            break;
                        case ("Address"):
                            baseQuery = baseQuery.OrderBy(x => x.地址);
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
                        case ("ClientName"):
                            baseQuery = baseQuery.OrderByDescending(x => x.客戶名稱);
                            break;
                        case ("CompanyNumber"):
                            baseQuery = baseQuery.OrderByDescending(x => x.統一編號);
                            break;
                        case ("CustomerTypeName"):
                            baseQuery = baseQuery.OrderByDescending(x => x.客戶類別.類別名稱);
                            break;
                        case ("Phone"):
                            baseQuery = baseQuery.OrderByDescending(x => x.電話);
                            break;
                        case ("Fax"):
                            baseQuery = baseQuery.OrderByDescending(x => x.傳真);
                            break;
                        case ("Address"):
                            baseQuery = baseQuery.OrderByDescending(x => x.地址);
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