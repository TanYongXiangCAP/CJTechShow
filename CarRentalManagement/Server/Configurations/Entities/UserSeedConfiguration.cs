using CarRentalManagement.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace CarRentalManagement.Server.Configurations.Entities
{
    public class UserSeedConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
            new ApplicationUser
            {
                Id = "3781efa7-66dc-47f0-860f-e506d04102e4",
                Email = "admin@localhost.com",
                NormalizedEmail = "ADMIN@LOCALHOST.COM",
                FirstName = "Admin",
                LastName = "User",
                UserName = "admin@localhost.com",
                NormalizedUserName = "ADMIN@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword1")
            },
            new ApplicationUser
            {
                Id = "fbc27c00-6bb6-4322-b2f2-deab42bc1420",
                Email = "chris@localhost.com",
                NormalizedEmail = "CHRIS@LOCALHOST.COM",
                FirstName = "chris",
                LastName = "ray",
                UserName = "chrisray@localhost.com",
                NormalizedUserName = "CHRISRAY@LOCALHOST.COM",
                PasswordHash = hasher.HashPassword(null, "P@ssword2")
            }
            );
        }
    }
}