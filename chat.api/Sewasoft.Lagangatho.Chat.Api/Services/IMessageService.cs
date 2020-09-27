using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sewasoft.Lagangatho.Chat.Api.Models;

namespace Sewasoft.Lagangatho.Chat.Api.Services
{
    public interface IMessageService
    {
        Task<List<Message>> GetMessagesAsync();
        Task<List<Message>> GetMessagesForChatRoomAsync(Guid roomId);
        Task<bool> AddMessageToRoomAsync(Guid roomId, Message message);
    }
}
