using Sewasoft.Lagangatho.Chat.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sewasoft.Lagangatho.Chat.Api.Services
{
    public interface IChatRoomService
    {
        Task<List<ChatRoom>> GetChatRoomsAsync();
        Task<bool> AddChatRoomAsync(ChatRoom newChatRoom);
    }
}
