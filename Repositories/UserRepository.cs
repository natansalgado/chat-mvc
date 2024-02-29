using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Context;
using mvc.Models;
using mvc.Repositories.Interfaces;

namespace mvc.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatContext _context;

        public UserRepository(ChatContext context)
        {
            _context = context;
        }

        public List<UserModel> GetAll()
        {
            return _context.Users.ToList();
        }

        public UserModel GetById(int id)
        {
            return _context.Users.Find(id);
        }

        public UserModel GetByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public UserModel Create(UserModel userModel)
        {
            _context.Users.AddAsync(userModel);
            _context.SaveChanges();

            return userModel;
        }

        public UserModel Update(UserModel userModel)
        {
            _context.Update(userModel);
            _context.SaveChanges();

            return userModel;
        }
    }
}