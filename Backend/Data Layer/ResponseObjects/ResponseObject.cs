using Data_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ResponseObjects
{
    public class ResponseObject
    {
        public string? Type { get; set; }
        public string? Description { get; set; }

        public ResponseObject(string type, string description)
        {
            Type = type;
            Description = description;
        }
    }
}
