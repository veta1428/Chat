using Chat.Auth;
using Chat.EF;
using Chat.Services;
using Microsoft.EntityFrameworkCore;

namespace Chat.Middlewares
{
    public class UserAccessorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ChatContext _context;

        public UserAccessorMiddleware(RequestDelegate next, ChatContext context)
        {
            _next = next;
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, IUserAccessor userAccessor)
        {
            var identity = context.User.Identity is not null && context.User.Identity.IsAuthenticated 
                ? (Identity)context.User.Identity 
                : null;

            if (identity is null)
            {
                userAccessor.SetUser(null);
            }
            else
            {
                var user = await _context.Users
                    .Where(u => u.Id == identity.Id)
                    .SingleAsync(CancellationToken.None);
                userAccessor.SetUser(user);
            }

            await _next.Invoke(context);
        }
    }
}
