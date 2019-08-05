using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Itreansition.Services.SearchService.Search;

namespace Itreansition.ViewModels
{
    public class SearchViewModel
    {
        public string StringForSearch { get; set; } 
        public List<SortedSearch> FoundInComments { get; set; }
        public List<SortedSearch> FoundInCompanies { get; set; }
        public List<SortedSearch> FoundInBonuses { get; set; }

    }
}
