using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class CompanyImage
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Img { get; set; }
    }
}
