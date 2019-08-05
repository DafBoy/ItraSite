using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class CompanyModel
    {
        public User OwnerCompany { get; set; }
        public Company CurrentCompany { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        //public  IEnumerable<LikesComment> LikesForCommets { get; set; }
        public  IEnumerable<Vote> LikesForCompanies { get; set; }

        public double Procent { get; set; }
        public IEnumerable<CompanyImage> ImagesCompany { get; set; }
    }
}
