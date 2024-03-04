using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Dtos;
using mvc.Models;

namespace mvc.Repositories.Interfaces
{
    public interface IUserRepository
    {
        List<UserModel> GetAll();
        UserModel GetById(int id);
        UserModel GetByUserName(string userName);
        UserModel GetByLogin(LoginDto loginDto);
        UserModel Create(UserModel userModel);
        UserModel Update(UserModel userModel);
    }
}