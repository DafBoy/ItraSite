using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string OwnerName { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public uint Like { get; set; } = 0;
        public uint Dislike { get; set; } = 0;

    }
}
