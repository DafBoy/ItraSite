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
        public int CurrentMoney { get; set; } = 0;
        public int NeedMoney { get; set; }
        public DateTime DateCreate { get; set; }
        public string Logo { get; set; }= "/img/ServerStaticImg/Logo.jpg";
        public string Video { get; set; } = "https://www.youtube.com/embed/p_ggdUq4n38";
        public short Raiting { get; set; } = 0;
        public string BrieflyAbout { get; set; }
        public string About { get; set; }
        public bool Moderation { get; set; } = false;
        public byte Rating { get; set; } = 0;

        public int GetPercentAmount()
        {
            int a= (int)Math.Round(((CurrentMoney / (double)(NeedMoney)) * 100.0), 1);

            return (int)Math.Round(((CurrentMoney / (double)(NeedMoney)) * 100.0), 1);

        }

    }
}
