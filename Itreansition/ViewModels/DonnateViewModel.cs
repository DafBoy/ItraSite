using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class DonnateViewModel
    {
        public Company Company { get; set; }
        public IEnumerable<Bonus> BonusList { get; set; }
        public User User { get; set; }
    }
}
