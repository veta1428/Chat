using Chat.EF;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ChatContext _chatContext;
        private readonly IUserAccessor _userAccessor;

        public AccountController(ChatContext chatContext, IUserAccessor userAccessor)
        {
            _chatContext = chatContext;
            _userAccessor = userAccessor;
        }

        [HttpGet]
        [Route("info")]
        public async Task<UserModel> GetUserInfo(int userId, CancellationToken cancellationToken)
        {
            var user = _userAccessor.CurrentUser;
            if (user == null)
            {
                throw new Exception("User is null");
            }


            return new UserModel(user.Id, user.FirstName, user.LastName);
        }
    }
}
