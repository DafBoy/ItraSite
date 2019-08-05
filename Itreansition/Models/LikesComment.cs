using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class LikesComment
    {
        public int Id { get; set; }
        public int CommentId { get; set; }

        public string OwnerLikeName { get; set; }

        public bool Like { get; set; }
    }
}
