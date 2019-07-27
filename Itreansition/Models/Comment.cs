using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int companyId { get; set; }
        public int ownerId { get; set; }
        public string ownerName { get; set; }
        public DateTime date { get; set; }
        public string text { get; set; }
        public uint like { get; set; }
        public uint dislike { get; set; }

    }
}
