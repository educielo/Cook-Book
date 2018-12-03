using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Models
{
    public class User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
