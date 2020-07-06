using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Readdit.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Readdit.Infrastructure.Ef
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Community> Communities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) 
        {

            builder.Entity<Link>()
                .HasMany(x => x.Comments)
                .WithOne(y => y.Link)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Link>()
            .HasMany(x => x.Votes)
            .WithOne(x => x.Link)
            .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(builder);
        }
    }
}
