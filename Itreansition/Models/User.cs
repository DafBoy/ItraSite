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

        public short status { get; set; }
        public uint Money { get; set; }
        public short Rating { get; set; }
        public DateTime DateCheck { get; set; }
    }
}
