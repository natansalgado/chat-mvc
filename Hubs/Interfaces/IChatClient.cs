using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Hubs.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message, string nameColor);
        Task ChatHistory(List<MessageModel> history);
    }
}