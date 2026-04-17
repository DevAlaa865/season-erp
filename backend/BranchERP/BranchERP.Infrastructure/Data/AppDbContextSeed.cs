using BranchERP.Domain.Entities;
using BranchERP.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BranchERP.Infrastructure.Data
{
    public static class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Permissions.Any())
            {
                var permissions = new List<Permission>
                {
                    // =========================
                    // Countries
                    // =========================
                    new Permission { Name = "View Countries",   Code = "Countries.View" },
                    new Permission { Name = "Create Country",  Code = "Countries.Create" },
                    new Permission { Name = "Edit Country",    Code = "Countries.Edit" },
                    new Permission { Name = "Delete Country",  Code = "Countries.Delete" },

                    // =========================
                    // Cities
                    // =========================
                    new Permission { Name = "View Cities",     Code = "Cities.View" },
                    new Permission { Name = "Create City",     Code = "Cities.Create" },
                    new Permission { Name = "Edit City",       Code = "Cities.Edit" },
                    new Permission { Name = "Delete City",     Code = "Cities.Delete" },

                    // =========================
                    // Branch Sales Daily (يومية المبيعات)
                    // =========================
                    new Permission { Name = "View Branch Sales Daily", Code = "BranchSalesDaily.View" },

                    // =========================
                    // Branch Daily Target (التارجت)
                    // =========================
                    new Permission { Name = "View Branch Daily Target", Code = "BranchDailyTarget.View" },

                    // =========================
                    // Returns (المرتجعات)
                    // =========================
                    new Permission { Name = "View Returns", Code = "Returns.View" },

                    // =========================
                    // Discounts (الخصومات)
                    // =========================
                    new Permission { Name = "View Discounts", Code = "Discounts.View" },

                    // =========================
                    // Finance (الإدارة المالية)
                    // =========================
                    new Permission { Name = "View Finance", Code = "Finance.View" },

                    // =========================
                    // Development (التطوير)
                    // =========================
                    new Permission { Name = "View Development", Code = "Development.View" },

                    // =========================
                    // General Management (الإدارة العامة)
                    // =========================
                    new Permission { Name = "View General Management", Code = "GeneralManagement.View" },

                    // =========================
                    // Branch Management (إدارة الفروع)
                    // =========================
                    new Permission { Name = "View Branch Management", Code = "BranchManagement.View" },

                    // =========================
                    // Sales (إدارة المبيعات)
                    // =========================
                    new Permission { Name = "View Sales", Code = "Sales.View" },

                    // =========================
                    // Permission Management (للأدمن فقط)
                    // =========================
                    new Permission { Name = "Manage Permissions", Code = "Permissions.Manage" },
                };

                await context.Permissions.AddRangeAsync(permissions);
                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedRolesAsync(RoleManager<ApplicationRole> roleManager)
        {
            var roles = new List<string>
            {
                "Admin",
                "BranchManagement",
                "Sales",
                "Accounts",
                "Returns",
                "Discounts",
                "Finance",
                "Development",
                "GeneralManagement",
                "IT"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new ApplicationRole { Name = role });
                }
            }
        }

        public static async Task SeedRolePermissionsAsync(AppDbContext context)
        {
            if (context.RolePermissions.Any())
                return; // ❗ لو فيه بيانات خلاص
            var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
            if (adminRole == null) return;

            var permissions = context.Permissions.ToList();

            // Admin ياخد كل الصلاحيات
            foreach (var perm in permissions)
            {
                if (!context.RolePermissions.Any(rp => rp.RoleId == adminRole.Id && rp.PermissionId == perm.Id))
                {
                    context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = adminRole.Id,
                        PermissionId = perm.Id
                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedAdminAsync(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            var adminUserName = "admin";
            var adminEmail = "admin@brancherp.local";
            var adminPassword = "Admin@123";

            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (!result.Succeeded)
                    return;

                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
