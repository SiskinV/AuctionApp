using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionApp.Models.Database{
    public class Auction{
        [Key]
        [Required]
        [Display(Name="ID")]
        public int id {get;set;}

        [Required]
        [Display(Name = "Name")]
        public string name {get;set;}

        [Required]
        [Display(Name = "Date of creation")]
        public DateTime dateCreate {get;set;}

        [Required]
        [Display(Name = "Date of opening")]
        public DateTime dateOpen {get;set;}

        [Required]
        [Display(Name = "Date of expiration")]
        public DateTime dateExpire {get;set;}

        [Required]
        [Display(Name = "Starting price")]
        public int startPrice {get;set;}

        [Required]
        [Display(Name = "Description")]
        public string description {get;set;}

        [Required]
        public int bidAmount {get;set;} = 0;


        // OVDE TREBA DA PROMENIS DA POCETNO STANJE BUDE DRAFT
        [Required]
        public string state {get;set;} = "DRAFT";

        [Required]
        public string userId {get;set;}

        public User user{get;set;}

        [Required]
        [Timestamp]
        public byte[] rowVersion{get;set;}

        public string lastBidder {get;set;} = "-";

        [Required]
        public byte[] image {get;set;}
    }

    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.Property(item => item.id).ValueGeneratedOnAdd();

            builder.HasOne<User>(item => item.user).WithMany(item => item.Auctions).HasForeignKey(item => new{item.userId});
        }
    }
}