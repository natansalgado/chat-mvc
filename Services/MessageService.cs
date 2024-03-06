using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Repositories.Interfaces;
using chatmvc.Services.Interfaces;
using mvc.Models;

namespace chatmvc.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public List<MessageModel> GetAll()
        {
            return _repository.GetAll();
        }

        public MessageModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public MessageModel Create(int userId, string message)
        {
            MessageModel messageModel = new()
            {
                UserId = userId,
                Message = message,
                Date = DateTime.Now
            };

            _repository.Create(messageModel);

            return GetById(messageModel.Id);
        }
    }
}