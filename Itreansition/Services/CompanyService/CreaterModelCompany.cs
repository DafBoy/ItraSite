using Itreansition.Models;
using Itreansition.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Services.CompanyService
{
    public class CreaterModelCompany
    {


        public  static CompanyModel GetCompanyById(int id,AppDbContext db)  //Создает модель для отправки во View Company/Index
        {
            CompanyModel temp = new CompanyModel() ;
            temp.CurrentCompany = db.Companies.FirstOrDefault(i => i.Id == id);
            if (temp.CurrentCompany == null) return null;
            temp.Comments = db.Comments.Where(c => c.CompanyId == id);
            temp.ImagesCompany=db.CompanyImages.Where(i => i.CompanyId == id);
            temp.OwnerCompany = db.Users.FirstOrDefault(u => u.UserName == temp.CurrentCompany.OwnerName);
            temp.Comments = db.Comments.Where(c => c.CompanyId == id);
            temp.LikesForCompanies = db.Votes.Where(i => i.CompanyId == id);
            temp.Procent= Math.Round(((temp.CurrentCompany.CurrentMoney / (double)(temp.CurrentCompany.NeedMoney)) * 100.0),1);
            return temp;

        
        

        }
    }
}
