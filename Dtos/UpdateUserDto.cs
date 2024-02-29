using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Dtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}