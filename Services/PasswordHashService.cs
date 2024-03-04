using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Services.Interfaces;

namespace mvc.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string providedPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
        }
    }
}