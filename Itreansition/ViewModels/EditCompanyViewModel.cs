using Itreansition.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class EditCompanyViewModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Range(500, 2140000000)]
        [Display(Name = "Amount of money needed")]
        public int NeedMoney { get; set; }
        public string Logo { get; set; } 
        [Required]
        [MinLength(25)]
        [MaxLength(190)]
        [Display(Name = "Briefly about the project")]
        public string BrieflyAbout { get; set; }
        [Required]
        [MinLength(50)]
        [MaxLength(800)]
        [Display(Name = "Full project description")]
        public string About { get; set; }
        public string OwnerName { get; set; }
        public int Id { get; set; }
        public string Video { get; set; }


        public static EditCompanyViewModel ParseCompanyInViewModel(Company company)
        {
            EditCompanyViewModel editCompanyViewModel = new EditCompanyViewModel();
            editCompanyViewModel.Name = company.Name;
            editCompanyViewModel.NeedMoney = company.NeedMoney;
            editCompanyViewModel.Logo = company.Logo;
            editCompanyViewModel.BrieflyAbout = company.BrieflyAbout;
            editCompanyViewModel.About = company.About;
            editCompanyViewModel.OwnerName = company.OwnerName;
            editCompanyViewModel.Video = company.Video;
            editCompanyViewModel.Id = company.Id;

            return editCompanyViewModel;

        }
    }


    
}
