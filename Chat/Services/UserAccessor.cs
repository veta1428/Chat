using Chat.Auth;
using Chat.EF;
using Chat.EF.Entities;

namespace Chat.Services
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ChatContext _context;

        public UserAccessor(IHttpContextAccessor httpContextAccessor, ChatContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;

            var httpContext = _httpContextAccessor.HttpContext ?? throw new ArgumentNullException(nameof(httpContextAccessor));

            var identity = httpContext.User.Identity is not null && httpContext.User.Identity.IsAuthenticated
                ? (Identity?)Activator.CreateInstance(typeof(Identity), httpContext.User.Identity)
                : null;

            if (identity is null)
            {
                SetUser(null);
            }
            else
            {
                var user = _context.Users
                    .Where(u => u.Id == identity.Id)
                    .Single();

                SetUser(user);
            }
        }

        private  User? _user;

        public User? CurrentUser { get => _user; set => _user = value; }

        public void SetUser(User? user)
        {
            CurrentUser = user;
        }
    }
}
