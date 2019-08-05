using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }

        public string OwnerName { get; set; }

    }
}
