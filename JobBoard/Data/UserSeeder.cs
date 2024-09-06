using JobBoard.Constants;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Data
{
    public class UserSeeder
    {
        public static async Task SeedUsersAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string adminEmail = configuration["AdminEmail"]!;
            string employerEmail = configuration["EmployerEmail"]!;
            string jobseekerEmail = configuration["JobseekerEmail"]!;
            string password = configuration["Password"]!;

            await CreateUserWithRole(userManager, adminEmail, password, role: Roles.Admin);
            await CreateUserWithRole(userManager, employerEmail, password, Roles.Employer);
            await CreateUserWithRole(userManager, jobseekerEmail, password, Roles.JobSeeker);
        }

        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed to create user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
