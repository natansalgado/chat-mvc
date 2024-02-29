using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Dtos;
using mvc.Models;

namespace mvc.Services.Interfaces
{
    public interface IUserService
    {
        List<UserModel> GetAll();
        UserModel GetById(int id);
        UserModel GetByUserName(string userName);
        UserModel Create(CreateUserDto createUserDto);
        UserModel Update(int id, UpdateUserDto updateUserDto);
    }
}