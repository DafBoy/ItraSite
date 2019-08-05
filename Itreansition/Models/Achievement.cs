using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public string OwnerName { get; set; }

        public bool MrLiker { get; set; }
        public bool Shot { get; set; }
        public bool Saint { get; set; }
        public bool Critic { get; set; }
        public bool Judge { get; set; }
        public bool Сheckmate { get; set; }
        public bool Closer { get; set; }
        public bool MrPiggyBank { get; set; }
    }
}
