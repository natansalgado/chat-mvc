using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Models;

namespace mvc.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserModel user);
    }
}