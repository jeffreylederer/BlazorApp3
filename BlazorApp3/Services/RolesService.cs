
using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazerApp1.Services
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<Role> _roles;
        private readonly DbSet<User> _users;
        private readonly DbSet<UserRole> _userRoles;

        public RolesService(IUnitOfWork uow)
        {
            _uow = uow;
            _uow.CheckArgumentIsNull(nameof(_uow));

            _roles = _uow.Set<Role>();
            _users = _uow.Set<User>();
            _userRoles = _uow.Set<UserRole>();
        }

        public Task<List<Role>> FindUserRolesAsync(int userId)
        {

            var userRolesQuery = from userRole in _userRoles
                        where userRole.UserId == userId
                        from role in _roles
                        where role.Id == userRole.RoleId
                        select role;



            //var userRolesQuery = from role in _roles
                
            //    from userRoles in role.UserRoles
            //    where userRoles.UserId == userId
            //    select role;

            return userRolesQuery.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<bool> IsUserInRole(int userId, string roleName)
        {

            var userRolesQuery = await  (from ur in _userRoles
                                 where ur.UserId == userId
                                 from role in _roles
                                 where role.Id == ur.RoleId 
                                 select role).ToListAsync();

            return userRolesQuery.Any(x=>x.Name == roleName);



            //var userRolesQuery = from role in _roles
                
            //    where role.Name == roleName
            //    from user in role.UserRoles
            //    where user.UserId == userId
            //    select role;
            //var userRole = await userRolesQuery.FirstOrDefaultAsync();
            //return userRole != null;
        }

        public async Task<List<User>> FindUsersInRoleAsync(string roleName)
        {
            var userRolesQuery = await (from ur in _userRoles
                                 from Role role in _roles
                                 where role.Name == roleName && role.Id == ur.RoleId
                                 from user in _users
                                 where user.Id == ur.UserId
                                 select user).ToListAsync();
            return userRolesQuery;

            //var roleUserIdsQuery = from role in _roles
            //    where role.Name == roleName
            //    from user in role.UserRoles
            //    select user.UserId;
            //return _users.Where(user => roleUserIdsQuery.Contains(user.Id))
            //    .ToListAsync();
        }
    }
}
