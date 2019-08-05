using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Services.CompanyService
{
    public class DellService
    {

        public static  void RemoveRelations(AppDbContext context,int idCompany)
        {

            context.Comments.RemoveRange(context.Comments.Where(i=>i.CompanyId==idCompany));
            context.Bonuses.RemoveRange(context.Bonuses.Where(i => i.CompanyId == idCompany));
            context.CompanyImages.RemoveRange(context.CompanyImages.Where(i => i.CompanyId == idCompany));
            context.SaveChanges();



        }
    }
}
