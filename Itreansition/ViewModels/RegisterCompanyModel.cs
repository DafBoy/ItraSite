using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class RegisterCompanyModel
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
        public string Logo { get; set; } = "/img/ServerStaticImg/Logo.jpg";
        [Required]
        [MinLength(25)]
        [MaxLength(190)]
        [Display(Name = "Briefly about the project")]
        public string BrieflyAbout { get; set; }
        [Required]
        [MinLength(50)]
        [MaxLength(2000)]
        [Display(Name = "Full project description")]
        public string About { get; set; }
        public string OwnerName { get; set; }
    }
}
