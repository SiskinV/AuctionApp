using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AuctionApp.Models.View{
    public class AuctionModel{  

        [Required]
        [Display(Name="Name")]
        public string name{get;set;}

        [Required]
        [Display(Name = "Date of opening")]
        public DateTime dateOpen{get;set;}

        [Required]
        [Display(Name = "Date of expiration")]
        public DateTime dateExpire{get;set;}

        [Required]
        [Display(Name = "Starting price")]
        public int startPrice {get;set;}

        [Required]
        [Display(Name = "Description")]
        public string description{get;set;}

        [Required]
        [Display(Name = "Image")]
        public IFormFile image{get;set;}
    }
}