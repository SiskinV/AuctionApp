using System.ComponentModel.DataAnnotations;
using AuctionApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApp.Models.View{
    public class EditModel{

        [Display(Name="First name")]
        public string firstName {get;set;}

        [Display(Name="Last name")]
        public string lastName {get;set;}

        [Display(Name="Gender")]
        public string gender {get;set;}

        [Display(Name="Email")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [Remote (controller: "User",action: nameof(UserController.isEmailUnique))]
        public string email{get;set;}

        [Display(Name="New password")]
        [DataType(DataType.Password)]
        public string password {get;set;}

        [Display(Name="Confirm new password")]
        [DataType(DataType.Password)]
        [Compare(nameof(password),ErrorMessage="New password and confirm new password must match!")]
        public string confirmPassword {get;set;}
     
    }
}