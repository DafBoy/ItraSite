using Itreansition.Models;
using Korzh.EasyQuery.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Services.SearchService
{
    public class Search
    {
        public class SortedSearch
        {
            public Company company { get; set; }
            public int Coincidences { get; set; }


        }


        //public static SearchSortedDictionary Find(string request, AppDbContext context)
        //{

        //    List<SortedSearch> ListComments = FindInComments(request, context);
        //    if (ListComments != null) sortedDictionary.SearchResult("Comments", ListComments.ToArray());

        //    sortedDictionary.SearchResult.Add("Bonuses", FindInBonuses(request, context));
        //    sortedDictionary.SearchResult.Add("Companies", FindInCompanies(request, context));

        //    return sortedDictionary;

        //}

        public static List<SortedSearch> FindInComments(string request, AppDbContext context)
        {
            List<SortedSearch> sorteds = new List<SortedSearch>();
            var comments = context.Comments.FullTextSearchQuery(request);
            foreach (var coment in comments)
            {
                SortedSearch temp = new SortedSearch();
                if (sorteds.FirstOrDefault(i => i.company.Id == coment.CompanyId) == null)
                {
                    temp.company = context.Companies.FirstOrDefault(i => i.Id == coment.CompanyId);
                    temp.Coincidences = comments.Count(i => i.CompanyId == coment.CompanyId);
                    sorteds.Add(temp);

                }
            }
            return sorteds;
        }
        public static List<SortedSearch> FindInBonuses(string request, AppDbContext context)
        {
            List<SortedSearch> sorteds = new List<SortedSearch>();
            var bonuses = context.Bonuses.FullTextSearchQuery(request);
            foreach (var bonus in bonuses)
            {
                SortedSearch temp = new SortedSearch();
                if (sorteds.FirstOrDefault(i => i.company.Id == bonus.CompanyId) == null)
                {
                    
                    temp.company = context.Companies.FirstOrDefault(i=>i.Id== bonus.CompanyId);
                    temp.Coincidences = bonuses.Count(i => i.CompanyId == bonus.CompanyId);
                    sorteds.Add(temp);

                }
            }
            return sorteds;
        }

        public static List<SortedSearch> FindInCompanies(string request, AppDbContext context)
        {
            List<SortedSearch> sorteds = new List<SortedSearch>();
            var companies = context.Companies.FullTextSearchQuery(request);
            foreach (var company in companies)
            {
                SortedSearch temp = new SortedSearch();
                if (sorteds.FirstOrDefault(i => i.company.Id == company.Id) == null)
                {
                    temp.company = company;
                    temp.Coincidences = companies.Count(i => i.Id == company.Id);
                    sorteds.Add(temp);

                }
            }
            return sorteds;
        }

    }
}
