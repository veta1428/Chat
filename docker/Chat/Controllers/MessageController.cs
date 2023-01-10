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
    public class MessageController : ControllerBase
    {
        private readonly ChatContext _chatContext;
        private readonly IUserAccessor _userAccessor;

        public MessageController(
            ChatContext libraryContext, 
            IUserAccessor userAccessor)
        {
            _chatContext = libraryContext;
            _userAccessor = userAccessor;
        }

        [HttpPost]
        [Route("send-message")]
        public async Task GetUsers([FromBody] SendTextMessageBody sendMessage, CancellationToken cancellationToken)
        {
            var sender = _userAccessor.CurrentUser!;

            var message = new Message() { UserFrom = sender, SentDateTime = DateTime.UtcNow, ChatId = sendMessage.ChatIdTo };

            var messageContent = new TextMessageContent() { Text = sendMessage.Text };

            message.MessageContent = messageContent;

            _chatContext.Messages.Add(message);

            await _chatContext.SaveChangesAsync(cancellationToken);
        }

        [HttpGet]
        [Route("get-messages/{chatId}")]
        public async Task<MessagesListModel> GetUsers(int chatId, CancellationToken cancellationToken)
        {
            var messageModels = await _chatContext.Messages
                .Where(m => m.ChatId == chatId)
                .Select(m => new MessageModel(
                    m.UserFromId, 
                    m.UserFrom!.FirstName, 
                    m.UserFrom!.LastName, 
                    m.SentDateTime, 
                    (m.MessageContent as TextMessageContent).Text))
                .ToListAsync(cancellationToken);

            return new MessagesListModel(messageModels);
        }
    }
}
