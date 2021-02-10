using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionApp.Models.Database{
    public class Transaction{
        [Key]
        [Required]
        [Display(Name="ID")]
        public int id{get;set;}

        [Required]
        [Display(Name="Date")]
        public System.DateTime date {get;set;}

        [Required]
        [Display(Name="Tokens")]
        public int tokens{get;set;}

        [Required]
        public int cost{get;set;}

        [Required]
        public string userId{get;set;}
        public User user{get;set;}
    }

    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.Property(item => item.id).ValueGeneratedOnAdd();

            builder.HasOne<User>(item => item.user).WithMany(item => item.Transactions).HasForeignKey(item => new {item.userId});
        }
    }
}