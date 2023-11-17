using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SayronPizzaMVC.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Infrastructure.Initializers
{
    internal static class SeedRoleAndUser
    {
        public static void SeedRolesAndUsers(ModelBuilder builder)
        {
            string ADMIN_ID = Guid.NewGuid().ToString();
            // any guid, but nothing is against to use the same one
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string USER_ROLE_ID = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = USER_ROLE_ID,
                Name = "user",
                NormalizedName = "USER"
            });

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                FirstName = "Nazar",
                LastName = "Kurylovych",
                UserName = "xvtnxjgbyv@gmail.com",
                NormalizedUserName = "xvtnxjgbyv@gmail.com",
                Email = "xvtnxjgbyv@gmail.com",
                NormalizedEmail = "xvtnxjgbyv@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Sayron450@"),
                SecurityStamp = string.Empty
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });
        }
       
    }
}
