using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sewasoft.Lagangatho.Chat.Api.Models;

namespace Sewasoft.Lagangatho.Chat.Api.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly ApplicationDbContext _context;

        public ChatRoomService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<List<ChatRoom>> GetChatRoomsAsync()
        {
            var chatRooms = await _context.ChatRooms.ToListAsync<ChatRoom>();

            return chatRooms;
        }

        public async Task<bool> AddChatRoomAsync(ChatRoom chatRoom)
        {
            chatRoom.Id = Guid.NewGuid();

            _context.ChatRooms.Add(chatRoom);

            var saveResults = await _context.SaveChangesAsync();

            return saveResults > 0;
        }
    }
}