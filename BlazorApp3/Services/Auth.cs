using BlazorApp3.Model;
using Microsoft.CodeAnalysis;

namespace BlazorApp3.Services
{
    public class AuthService : IAuthService
    {

        public AuthService()
        {
           
        }
       

        public AuthResponse? Login(AuthRequest request, IConfiguration configuration)
        {

            JwtUtils util = new JwtUtils(configuration);
            using (var db = new TournamentContext())
            {
                var user = db.Users.Where(x => x.Username == request.Username && x.Password == request.Password).ToList().FirstOrDefault();
                if(user != null)
                {
                    List<string> rules = (from ur in db.UserRoles
                            join roles in db.Roles on ur.RoleId equals roles.Id
                            where ur.UserId == user.Id
                            select roles.Name).ToList();

                    //if (user != null && user.Roles != null && !string.IsNullOrWhiteSpace(user.Roles))
                    //    rules.Add(user.Roles);
                    var response = new AuthResponse()
                    {
                        UserId = user.Id.ToString(),
                        Username = user.Username,
                        Email = user.Username,
                        Roles = rules,
                        Token = util.GenerateToken(user.Id.ToString(), user.Username, user.Username, rules)
                    };
                    
                    
                    return response;
                }
            }
            return null;
        }

        public bool Register(RegisterRequest request)
        {
            try
            {
                using (var db = new TournamentContext())
                {
                    List<string> roles = new List<string>();
                    if (!string.IsNullOrWhiteSpace(request.Role))
                        roles.Add(request.Role);
                    var user = new User()
                    {
                        Username = request.Username,
                        Password = request.Password,
                     };
                    db.Users.Add(user);
                    db.SaveChanges();
                    var role = db.Roles.Where(x=>x.Name == request.Role).FirstOrDefault();
                    if (role != null)
                    {
                        var userrole = new UserRole() { RoleId = role.Id, UserId = user.Id };
                        db.UserRoles.Add(userrole);
                        db.SaveChanges();
                    }
                }
                return true;
            }
            catch (Exception) { }
            return false;
        }
    }
}
