using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Hubs.Interfaces
{
    public interface IChatClient
    {
        Task ReceiveMessage(string userName, string message);
        Task ChatHistory(List<MessageModel> history);
    }
}