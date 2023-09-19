using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SayronPizzaMVC.Core.Entites.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Infrastructure.Initializers
{
    public static class UsersAndRolesInitializer
    {
        public static void SeedUserAndRoles(ModelBuilder builder)
        {
            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });

            var hasher = new PasswordHasher<AppUser>();
            builder.Entity<AppUser>().HasData(new AppUser
            {
                Id = ADMIN_ID,
                FirstName = "Nazar",
                LastName = "Kurylovych",
                UserName = "xvtnxjgbyv@gmail.com",
                Email = "xvtnxjgbyv@gmail.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Sayron450@"),
                PhoneNumber = "+xx(xxx)xxx-xx-xx",
                PhoneNumberConfirmed = true,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
