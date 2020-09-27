using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Sewasoft.Lagangatho.Chat.Api.Services;
using System;
using Sewasoft.Lagangatho.Chat.Api.Models;

namespace Sewasoft.Lagangatho.Chat.Api.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
        public int UsersOnline;

        public ChatHub(IChatRoomService chatRoomService, IMessageService messageService)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
        }

        public async Task SendMessage(Guid roomId, string user, string message)
        {
            Message m = new Message()
            {
                RoomId = roomId,
                Contents = message,
                UserName = user
            };

            await _messageService.AddMessageToRoomAsync(roomId, m);
            await Clients.All.SendAsync("ReceiveMessage", user, message, roomId, m.Id, m.PostedAt);
        }

        public async Task AddChatRoom(string roomName)
        {
            ChatRoom chatRoom = new ChatRoom()
            {
                Name = roomName
            };

            await _chatRoomService.AddChatRoomAsync(chatRoom);
            await Clients.All.SendAsync("NewRoom", roomName, chatRoom.Id);
        }

        public void LeaveGroup(string roomName)
        {
            this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, roomName);
        }

        public void JoinRomm(string roomName)
        {
            this.Groups.AddToGroupAsync(this.Context.ConnectionId, roomName);
        }

        public override async Task OnConnectedAsync()
        {
            UsersOnline++;
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            UsersOnline--;
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }
    }
}