using backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.Data {

    public class ApplicationDBContext : DbContext{

        public ApplicationDBContext(DbContextOptions options ): base(options){

        }

        // DATA Collection
        public DbSet<User> Users { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<Bid> Bids { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Auction)
            .WithMany(a => a.Bids)
            .HasForeignKey(b => b.AuctionId);
    }
    }
}