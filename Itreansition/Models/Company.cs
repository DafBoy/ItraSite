using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public string OwnerName { get; set; }
        public uint CurrentMoney { get; set; } = 0;
        public uint NeedMoney { get; set; }
        public DateTime DateCreate { get; set; }
        public string Logo { get; set; }= "/img/Logo.jpg";
        public string Images { get; set; } = "";
        public string Video { get; set; } = "";
        public short Raiting { get; set; } = 0;
        public string BrieflyAbout { get; set; }
        public string About { get; set; }
        public bool Moderation { get; set; } = false;

    }
}
