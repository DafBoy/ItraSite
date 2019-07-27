using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class AccountViewModels
    {
        public IEnumerable<Company> ListCompany { get; set; }
        public User AccountUser { get; set; }

    }



}
