using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Bonus
    {
        public int Id { get; set; }
        public string BonusName { get; set; }
        public int CompanyId { get; set; }
        public string Image { get; set; } = "/img/ServerStaticImg/NonBonusIMG.png";

        public int Sum { get; set; }
        public ushort Amount { get; set; } = 0;

        public string Description { get; set; }

    }
}
