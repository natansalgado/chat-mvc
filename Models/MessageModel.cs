using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mvc.Models
{
    public class MessageModel
    {
        public string Name { get; set; }
        public string Message { get; set; }
        public string NameColor { get; set; }

        public MessageModel(string name, string message, string nameColor)
        {
            Name = name;
            Message = message;
            NameColor = nameColor;
        }
    }
}