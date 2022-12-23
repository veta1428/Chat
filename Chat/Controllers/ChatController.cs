using Chat.EF;
using Chat.EF.Entities;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Chat.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public ChatController(ChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        [HttpPost]
        [Route("create-chat")]
        public async Task CreateChat([FromBody] CreateChatModel createChatModel, CancellationToken cancellationToken)
        {
            // will be extracted from cookie further
            var userWhoCreatesChat = await _chatContext.Users.FirstAsync();

            var usersToCreateChatWith = await _chatContext.Users.Where(user => createChatModel.UserIds.Contains(user.Id)).ToListAsync(cancellationToken);

            usersToCreateChatWith.Add(userWhoCreatesChat);

            var chat = new Chat.EF.Entities.Chat() { CreatedDateTime = DateTime.UtcNow };

            var chatUsers = usersToCreateChatWith.Select(user => new ChatUser() { Chat = chat, User = user} );

            _chatContext.Chats.Add(chat);

            _chatContext.ChatUsers.AddRange(chatUsers);

            await _chatContext.SaveChangesAsync(cancellationToken);
        }

        [HttpGet]
        [Route("get-chats/{userId}")]
        public async Task<ChatListModel> GetChatsForUser(int userId, CancellationToken cancellationToken)
        {
            var chatModels = await _chatContext.Chats
                .Where(chat => chat.ChatUsers.Any(cu => cu.UserId == userId))
                .Select(chat => new ChatModel(chat.Id, chat.ChatUsers.Count() == 1 ? chat.ChatUsers.Single().User!.FullName : "No name group chat", chat.CreatedDateTime))
                .ToArrayAsync(cancellationToken);

            return new ChatListModel(chatModels);
        }
    }
}
