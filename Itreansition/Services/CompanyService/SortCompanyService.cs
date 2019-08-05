using Itreansition.Models;
using Itreansition.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Services.CompanyService
{
    public class SortCompanyService
    {
        public static List<Company> SortNew(AppDbContext context)
        {
             var sortedCompanies =context.Companies.OrderByDescending(d => d.DateCreate).ToList();
            List<Company> topNew=new List<Company>();

             for(var i=0;(i<sortedCompanies.Count)&& (i<12);i++)
            {
                topNew.Add(sortedCompanies[i]);
            }
            return topNew;
        }
        public static List<Company> SortRich(AppDbContext context)
        {
            var sortedCompanies = context.Companies.OrderByDescending(c=>c.CurrentMoney ).ToList();
            List<Company> topRich = new List<Company>();



            for (var i = 0; (i < sortedCompanies.Count) && (i < 12); i++)
            {
                topRich.Add(sortedCompanies[i]);
            }
            return topRich;
        }

    }
}
