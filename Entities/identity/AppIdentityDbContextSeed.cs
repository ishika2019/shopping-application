using Microsoft.AspNetCore.Identity;

namespace project.Entities.identity
{
    public class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {

                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    Address = new Address
                    {
                        FirstName = "bob",
                        LastName = "bobity",
                        Street = "10 time",
                        City ="NewYork",
                        State = "NY",
                        ZipCode = "187869"

                    }

                };
                await userManager.CreateAsync(user,"Bob@1234567");

            }
        }
    }
}
