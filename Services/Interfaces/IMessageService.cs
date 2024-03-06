using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace chatmvc.Services.Interfaces
{
    public interface IMessageService
    {
        List<MessageModel> GetAll();
        MessageModel GetById(int id);
        MessageModel Create(int userId, string message);
    }
}