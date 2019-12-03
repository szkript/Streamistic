using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SilkVideo.Models;

namespace SilkVideo
{
    public class SilkVideoContext : IdentityDbContext<User>
    {
        public DbSet<Video> Videos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public SilkVideoContext(DbContextOptions<SilkVideoContext> options) :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SilkVideo;Trusted_Connection=True;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Video>()
                .Property(b => b.Id)
                .IsRequired();

            modelBuilder.Entity<Comment>()
                .Property(b => b.Id)
                .IsRequired();
        }
    }
}
