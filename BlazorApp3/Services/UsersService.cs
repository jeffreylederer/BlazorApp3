using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazerApp3.Models.DTOs;
using BlazerApp3.Services;
using BlazorApp3.Model;
using Microsoft.EntityFrameworkCore;


namespace BlazerApp3.Services
{
    public class UsersService : IUsersService
    {
        //private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _users;
        private readonly ISecurityService _securityService;

        private bool _isDisposed;
        private readonly TournamentContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _mapperConfiguration;

        public UsersService(ISecurityService securityService, TournamentContext dbContext, IMapper mapper)
        {
            //_uow = uow;
            //_uow.CheckArgumentIsNull(nameof(_uow));

            _users = dbContext.Set<User>();

            _securityService = securityService;
            _securityService.CheckArgumentIsNull(nameof(_securityService));

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mapperConfiguration = mapper.ConfigurationProvider;
        }

        public async ValueTask<User> FindUserAsync(int userId)
        {
            User? user;
            using(var context =  new TournamentContext())
            {
                user= await context.Users.FindAsync(userId);
            }
            return user;
        }

        public Task<User> FindUserAsync(string username, string password)
        {
            var passwordHash = _securityService.GetSha256Hash(password);
            return _users.FirstOrDefaultAsync(x => x.Username == username && x.Password == passwordHash);
        }

        public async Task<string> GetSerialNumberAsync(int userId)
        {
            User? user;
            using (var context = new TournamentContext())
            {
                user = await context.Users.FindAsync(userId);
            }
            return user.SerialNumber;
        }

        public async Task UpdateUserLastActivityDateAsync(int userId)
        {
            User? user;
            using (var context = new TournamentContext())
            {
                user = await context.Users.FindAsync(userId);
                if (user == null)
                    return;

                if (user.LastLoggedIn != null)
                {
                    var updateLastActivityDate = TimeSpan.FromMinutes(2);
                    var currentUtc = DateTimeOffset.UtcNow;
                    var timeElapsed = currentUtc.Subtract(user.LastLoggedIn.Value);
                    if (timeElapsed < updateLastActivityDate)
                    {
                        return;
                    }
                }
                user.LastLoggedIn = DateTimeOffset.UtcNow;
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserRegisterDTO> CreateUserAsync(UserRegisterDTO userRegisterDto)
        {
            var newUser = _mapper.Map<User>(userRegisterDto);

            try
            {
                newUser.LastLoggedIn = null;
                newUser.Password = _securityService.GetSha256Hash(userRegisterDto.Password);
                newUser.SerialNumber = Guid.NewGuid().ToString("N");
                newUser.IsActive = true;
                var addedUser = await _dbContext.Users.AddAsync(newUser);
                await _dbContext.SaveChangesAsync();
                UserRole item = new UserRole()
                {
                    RoleId = userRegisterDto.Role,
                    UserId = newUser.Id
                };
                await _dbContext.UserRoles.AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return _mapper.Map<UserRegisterDTO>(addedUser.Entity);
            }
            catch (Exception ex) 
            {
                var mess = ex.Message;
            }
            return new UserRegisterDTO();
           
        }

        public IAsyncEnumerable<UserRegisterDTO> GetAllUsersAsync()
        {
            return _dbContext.Users
                .ProjectTo<UserRegisterDTO>(_mapperConfiguration)
                .AsAsyncEnumerable();
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                try
                {
                    if (disposing)
                    {
                        _dbContext.Dispose();
                    }
                }
                finally
                {
                    _isDisposed = true;
                }
            }
        }
    }
}
