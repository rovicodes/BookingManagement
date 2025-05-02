using BookingManagement.Application.Common.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingManagement.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(AppConstants.Role_Admin))
                await roleManager.CreateAsync(new IdentityRole(AppConstants.Role_Admin));

            if (!await roleManager.RoleExistsAsync(AppConstants.Role_Customer))
                await roleManager.CreateAsync(new IdentityRole(AppConstants.Role_Customer));
        }
    }
}
