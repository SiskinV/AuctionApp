using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Models.Database{
    public class AuctionAppContext : IdentityDbContext<User>{

        public DbSet<Transaction> transactions{get;set;}
        public DbSet<Auction> auctions{get;set;}

        public AuctionAppContext (DbContextOptions options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new AuctionConfiguration());
        }
    }
}