using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Services.Interfaces
{
    public interface IPasswordHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}