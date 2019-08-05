using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.ViewModels
{
    public class UserSettingViewModel
    {
        public string UserName { get; set; }
        public string Avatar { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string NewPassword { get; set; }

    }
}
