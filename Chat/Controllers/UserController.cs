using Chat.EF;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ChatContext _chatContext;
        private readonly IUserAccessor _userAccessor;

        public UserController(
            ChatContext libraryContext,
            IUserAccessor userAccessor)
        {
            _chatContext = libraryContext;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser!;

            return await _chatContext.Users
                .Where(u => u.Id != user.Id)
                .Select(user => new UserModel(user.Id, user.FirstName, user.LastName))
                .ToArrayAsync(cancellationToken);
        }
    }
}
