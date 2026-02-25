using Deep_lines_Backend.DAL.Models;
using Deep_lines_Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Deep_lines_Backend.DAL.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {

         

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Projects>()
                .HasOne(p => p.user)
                .WithMany(u => u.Published_Projects)
                .HasForeignKey(p => p.User_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Projects>()
                .HasOne(p => p.sector)
                .WithMany(u => u.Related_Projects)
                .HasForeignKey(p => p.Sector_Id)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Blog>()
                .HasOne(b => b.user)
                .WithMany(u => u.Published_Blogs)
                .HasForeignKey(b => b.User_Id)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Sector>()
                .HasOne(s => s.published_user)
                .WithMany(u => u.Published_Sectors)
                .HasForeignKey(s => s.User_Id)
                .OnDelete(DeleteBehavior.NoAction); 
            modelBuilder.Entity<Product>()
                .HasOne(p=> p.published_user)
                .WithMany(u=>u.Published_Products)
                .HasForeignKey(p=> p.User_Id)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Review> Reviwe { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


    }
}
