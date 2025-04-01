using Microsoft.EntityFrameworkCore;
using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=OAS;Integrated Security=True;TrustServerCertificate=True");
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Review>(entity =>
        //    {
        //        entity.ToTable("Review");
        //        entity.HasKey(e => e.ReviewID);
        //        entity.HasOne(e => e.User)
        //              .WithMany(u => u.Reviews)
        //              .HasForeignKey(e => e.UserID);
        //    });
        //    modelBuilder.Entity<Bid>(entity =>
        //    {
        //        entity.ToTable("Bids");
        //        entity.HasKey(e => e.BidID);
        //        entity.HasOne(e => e.Auction)
        //              .WithMany(a => a.Bids)
        //              .HasForeignKey(e => e.AuctionID);
        //    entity.HasOne(e => e.Buyer)
        //              .WithMany(u => u.Bids)
        //              .HasForeignKey(e => e.BuyerID);
        //    });
        //}

        public DbSet<Product> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
