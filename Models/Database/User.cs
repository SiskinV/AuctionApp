using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionApp.Models.View;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AuctionApp.Models.Database{
    public class User : IdentityUser {

        [Required]
        [Display(Name="First name")]
        public string firstName {get;set;}

        [Required]
        [Display(Name = "Last name")]
        public string lastName {get;set;}

        [Required]
        [Display(Name = "Gender")]
        public string gender {get;set;}

        [Required]
        [Display(Name="Tokens")]
        public int tokens{get; set;} = 0;

        public bool deleted { get; set; } = false;

        public ICollection<Transaction> Transactions {get;set;}

        public ICollection<Auction> Auctions {get;set;}

    }

    public class UserProfile:Profile{
        public UserProfile(){
            base.CreateMap<RegisterModel,User>()
                .ForMember(
                    destination => destination.Email,
                    options => options.MapFrom(data => data.email)
                ).ForMember(
                    destination => destination.UserName,
                    options => options.MapFrom(data => data.userName)
                ).ReverseMap();
        }
    }
}