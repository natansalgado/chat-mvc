using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using mvc.Hubs.Interfaces;
using mvc.Models;

namespace mvc.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private static readonly List<MessageModel> _chatHistory = new();

        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ChatHistory(_chatHistory);

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message, string nameColor)
        {
            MessageModel messageModel = new(user, message, nameColor);

            _chatHistory.Add(messageModel);

            await Clients.All.ReceiveMessage(user, message, nameColor);
        }
    }
}