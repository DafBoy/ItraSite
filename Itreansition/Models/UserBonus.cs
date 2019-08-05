using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class UserBonus
    {
        public int Id { get; set; }
        public string BonusName { get; set; }
        public string OwnerName { get; set; }
        public string Image { get; set; } = "/img/ServerStaticImg/NonBonusIMG.png";

        public int Sum { get; set; }

        public string Description { get; set; }
    }
}
