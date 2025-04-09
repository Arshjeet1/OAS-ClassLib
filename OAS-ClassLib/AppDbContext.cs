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
            optionsBuilder.UseSqlServer("Data Source=LTIN593301;Initial Catalog=OAS;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Product - ProductImage relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductImages)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId);

            // Product - Auction relationship
            modelBuilder.Entity<Product>()
                .HasMany(p => p.Auctions)
                .WithOne(a => a.Product)
                .HasForeignKey(a => a.ProductId);

            // Auction - Bid relationship
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Bids)
                .WithOne(b => b.Auction)
                .HasForeignKey(b => b.AuctionID);

            // Auction - Transaction relationship
            modelBuilder.Entity<Auction>()
                .HasMany(a => a.Transactions)
                .WithOne(t => t.Auction)
                .HasForeignKey(t => t.AuctionID);

            // User - Transaction relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Transactions)
                .WithOne(t => t.Buyer)
                .HasForeignKey(t => t.BuyerID);

            // User - Review relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserID);

            // Ignore the conflicting navigation property
            modelBuilder.Entity<Review>()
                .Ignore(r => r.TargetUser);

            // User - Bid relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Bids)
                .WithOne(b => b.Buyer)
                .HasForeignKey(b => b.BuyerID);
        }
    }
}
