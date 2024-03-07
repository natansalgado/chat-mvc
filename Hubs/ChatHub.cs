using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using mvc.Hubs.Interfaces;
using mvc.Models;

namespace mvc.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IMessageService _messageService;

        public ChatHub(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public override async Task OnConnectedAsync()
        {
            var chatHistory = _messageService.GetAll();
            await Clients.Client(Context.ConnectionId).ChatHistory(chatHistory);

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(int userId, string message)
        {
            MessageModel messageModel = _messageService.Create(userId, message);

            await Clients.All.ReceiveMessage(messageModel.User, message);
        }
    }
}