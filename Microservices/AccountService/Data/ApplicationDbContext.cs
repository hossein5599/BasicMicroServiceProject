using AccountService.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AccountService.Data;
public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Core Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Core Identity table names and more.

        builder.Entity<User>(entity => entity.ToTable(name: "Users"));

        // To Ignore And Not Creating The Default tables Of User Identity 
        builder.Entity<User>(entity =>
        {
            entity.Ignore(e => e.TwoFactorEnabled);
            entity.Ignore(e => e.AccessFailedCount);
            entity.Ignore(e => e.LockoutEnabled);
            entity.Ignore(e => e.ConcurrencyStamp);
            entity.Ignore(e => e.NormalizedUserName);
            entity.Ignore(e => e.NormalizedEmail);
            entity.Ignore(e => e.LockoutEnd);
            entity.Ignore(e => e.SecurityStamp);
            entity.Ignore(e => e.PhoneNumberConfirmed);
            entity.Ignore(e => e.EmailConfirmed);
        });

        builder.Ignore<IdentityRole>();
        builder.Ignore<IdentityUserRole<string>>();
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
    }

}