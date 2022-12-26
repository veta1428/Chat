using Chat.EF;
using Chat.EF.Entities;
using Chat.Models;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _chatContext;
        private readonly IUserAccessor _userAccessor;

        public ChatController(ChatContext chatContext, IUserAccessor userAccessor)
        {
            _chatContext = chatContext;
            _userAccessor = userAccessor;
        }

        [HttpPost]
        [Route("create-chat")]
        public async Task CreateChat([FromBody] CreateChatModel createChatModel, CancellationToken cancellationToken)
        {
            var userWhoCreatesChat = _userAccessor.CurrentUser!;

            var usersToCreateChatWith = await _chatContext.Users.Where(user => createChatModel.UserIds.Contains(user.Id)).ToListAsync(cancellationToken);

            usersToCreateChatWith.Add(userWhoCreatesChat);

            var chat = new Chat.EF.Entities.Chat() { CreatedDateTime = DateTime.UtcNow };

            var chatUsers = usersToCreateChatWith.Select(user => new ChatUser() { Chat = chat, User = user} );

            _chatContext.Chats.Add(chat);

            _chatContext.ChatUsers.AddRange(chatUsers);

            await _chatContext.SaveChangesAsync(cancellationToken);
        }

        [HttpGet]
        [Route("get-chats")]
        public async Task<ChatListModel> GetChatsForUser(CancellationToken cancellationToken)
        {
            int userId = _userAccessor.CurrentUser!.Id;

            var chatModels = await _chatContext.Chats
                .Where(chat => chat.ChatUsers.Any(cu => cu.UserId == userId))
                .Select(chat => new ChatModel(chat.Id, chat.ChatUsers.Count() == 1 ? chat.ChatUsers.Single().User!.FullName : "No name group chat", chat.CreatedDateTime))
                .ToArrayAsync(cancellationToken);

            return new ChatListModel(chatModels);
        }
    }
}
