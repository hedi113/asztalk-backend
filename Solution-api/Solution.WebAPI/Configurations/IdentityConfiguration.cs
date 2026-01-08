using Microsoft.AspNetCore.Identity;
using Solution.Domain.Database;
using Solution.Domain.Database.Entities;

namespace Solution.WebAPI.Configurations;

public static class IdentityConfiguration
{
    extension(IHostApplicationBuilder builder)
    {
        public IHostApplicationBuilder UseIdentity()
        {
            builder.Services.AddDefaultIdentity<UserEntity>(options =>
            {
                // password
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;

                // lockout
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // user
                options.User.RequireUniqueEmail = true;

                // sign-in
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            return builder;
        }
    }
}
