using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Models.Database{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>{

    public static class Roles{
        public static IdentityRole administrator = new IdentityRole(){
            Name="Administrator",
            NormalizedName = "ADMINISTRATOR"
        };
        public static IdentityRole user = new IdentityRole(){
                Name = "User",
                NormalizedName = "USER"
        };
    }

    
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                Roles.user,
                Roles.administrator
            );
        }
    }
}