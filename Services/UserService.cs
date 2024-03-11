using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Dtos;
using mvc.Exceptions;
using mvc.Models;
using mvc.Repositories.Interfaces;
using mvc.Services.Interfaces;

namespace mvc.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHashService _passwordHashService;

        public UserService(IUserRepository repository, IPasswordHashService passwordHashService)
        {
            _repository = repository;
            _passwordHashService = passwordHashService;
        }

        public List<UserModel> GetAll()
        {
            return _repository.GetAll();
        }

        public UserModel GetById(int id)
        {
            return _repository.GetById(id);
        }

        public UserModel GetByUserName(string userName)
        {
            return _repository.GetByUserName(userName);
        }

        public UserModel Create(CreateUserDto createUserDto)
        {
            CheckUserName(createUserDto.UserName);

            UserModel userModel = new()
            {
                UserName = createUserDto.UserName,
                Password = _passwordHashService.HashPassword(createUserDto.Password)
            };

            return _repository.Create(userModel);
        }

        public UserModel Update(int id, UpdateUserDto updateUserDto)
        {
            UserModel userById = GetById(id) ?? throw new ChatException(404, "Usuário não encontrado!");

            if (updateUserDto.UserName != userById.UserName)
                CheckUserName(updateUserDto.UserName);

            if (updateUserDto.UserName != null) userById.UserName = updateUserDto.UserName;
            if (updateUserDto.Password != null) userById.Password = _passwordHashService.HashPassword(updateUserDto.Password);
            if (updateUserDto.Avatar != null) userById.Avatar = updateUserDto.Avatar;

            return _repository.Update(userById);
        }

        private void CheckUserName(string userName)
        {
            UserModel userByUserName = GetByUserName(userName);

            if (userByUserName != null) throw new ChatException(409, "Esse nome de usuário já está em uso, tente outro.");
        }
    }
}