using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace chatmvc.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        List<MessageModel> GetAll();
        MessageModel GetById(int id);
        MessageModel Create(MessageModel message);
    }
}