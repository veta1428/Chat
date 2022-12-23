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
    public class MessageController : ControllerBase
    {
        private readonly ChatContext _chatContext;

        public MessageController(ChatContext libraryContext)
        {
            _chatContext = libraryContext;
        }

        [HttpPost]
        [Route("send-message")]
        public async Task GetUsers([FromBody] SendTextMessageBody sendMessage, CancellationToken cancellationToken)
        {
            // will be extracted from cookie further
            var sender = await _chatContext.Users.FirstAsync();

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
                    ((TextMessageContent)m.MessageContent).Text))
                .ToListAsync(cancellationToken);

            return new MessagesListModel(messageModels);
        }
    }
}
