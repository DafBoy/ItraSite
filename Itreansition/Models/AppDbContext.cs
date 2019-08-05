using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Itreansition.Models
{
    public class AppDbContext:IdentityDbContext<User>
    {

        public DbSet<Company> Companies { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<CompanyImage> CompanyImages { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikesComment> LikesComments { get; set; }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<Bonus> Bonuses { get; set; }

        public DbSet<UserBonus> UserBonuses { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }


    }
}
