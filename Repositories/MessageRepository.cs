using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmvc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using mvc.Context;
using mvc.Models;

namespace chatmvc.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatContext _context;

        public MessageRepository(ChatContext context)
        {
            _context = context;
        }

        public List<MessageModel> GetAll()
        {
            return _context.Messages
            .Include(x => x.User)
            .Select(x => new MessageModel
            {
                Id = x.Id,
                Message = x.Message,
                Date = x.Date,
                UserId = x.UserId,
                User = new UserModel
                {
                    Id = x.User.Id,
                    UserName = x.User.UserName,
                    Avatar = x.User.Avatar
                }
            })
            .OrderBy(x => x.Date)
            .ToList();
        }

        public MessageModel GetById(int id)
        {
            return _context.Messages
            .Include(x => x.User)
            .Select(x => new MessageModel
            {
                Id = x.Id,
                Message = x.Message,
                Date = x.Date,
                UserId = x.UserId,
                User = new UserModel
                {
                    Id = x.User.Id,
                    UserName = x.User.UserName,
                    Avatar = x.User.Avatar
                }
            })
            .FirstOrDefault(x => x.Id == id);
        }

        public MessageModel Create(MessageModel message)
        {
            _context.Messages.Add(message);
            _context.SaveChanges();

            return message;
        }
    }
}