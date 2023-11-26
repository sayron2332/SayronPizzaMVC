using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SayronPizzaMVC.Core.Entites.Category;
using SayronPizzaMVC.Core.Entites.Product;
using SayronPizzaMVC.Core.Entites.User;
using SayronPizzaMVC.Infrastructure.Initializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Infrastructure.Context
{
    internal class AppDbContext : IdentityDbContext
    {
        public AppDbContext() : base() { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedRoleAndUser.SeedRolesAndUsers(builder);
          
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<AppCategory> AppCatogories { get; set; }
        DbSet<AppProduct> AppProducts { get; set; }

    }
}