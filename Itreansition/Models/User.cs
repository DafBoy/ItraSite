using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }

        public bool Status { get; set; }
        public string Avatar { get; set; } ="/img/ServerStaticImg/NonAvatarIMG.png";
        public int Money { get; set; } public User() { this.Money = 1000000; }
        public byte Rating { get; set; }
        public DateTime DateCheck { get; set; }
    }
}
