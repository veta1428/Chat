using Chat.EF;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public UserController(ChatContext libraryContext)
        {
            _chatContext = libraryContext;
        }

        [HttpGet]
        [Route("get-users")]
        public async Task<IEnumerable<UserModel>> GetUsers(CancellationToken cancellationToken)
        {
            return await _chatContext.Users
                .Select(user => new UserModel(user.Id, user.FirstName, user.LastName))
                .ToArrayAsync(cancellationToken);
        }
    }
}
