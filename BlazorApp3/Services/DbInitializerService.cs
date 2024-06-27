using BlazerApp3.Services;
using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazerApp3.Infralayer;

    public class DbInitializerService : IDbInitializerService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ISecurityService _securityService;

        public DbInitializerService(
            IServiceScopeFactory scopeFactory,
            ISecurityService securityService)
        {
            _scopeFactory = scopeFactory;
            _scopeFactory.CheckArgumentIsNull(nameof(_scopeFactory));

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));
        }

       

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TournamentContext>())
                {
                    if (!context.Roles.Any())
                    {

                        context.Roles.Add(new Role() { Name = "Observer", Id=1 });
                        context.Roles.Add(new Role() { Name = "Scorer", Id = 2 });
                        context.Roles.Add(new Role() { Name = "Admin", Id = 3 });
                        context.Roles.Add(new Role() { Name = "SiteAdmin", Id = 4 });
                        context.SaveChanges();
                    }

                    // Add Admin user
                    if (context.Users.ToList().Count == 0)
                    {
                        var adminUser = new User
                        {
                            Username = "SiteAdmin",
                            DisplayName = "Cheif Admin",
                            IsActive = true,
                            LastLoggedIn = null,
                            Password = _securityService.GetSha256Hash("password"),
                            SerialNumber = Guid.NewGuid().ToString("N")
                        };
                        context.Add(adminUser);
                        context.SaveChanges();
                        context.UserRoles.Add(new UserRole()
                        {
                            RoleId=4,
                            UserId=adminUser.Id
                        });
                        context.SaveChanges();
                }
            }
        }
    }
}
