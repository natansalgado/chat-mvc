using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}