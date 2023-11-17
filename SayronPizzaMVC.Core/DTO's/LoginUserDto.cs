using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayronPizzaMVC.Core.DTO_s
{
    public class LoginUserDto
    {
        public string Email { get; set;}
        public string Password { get; set;}
        public bool RememberMe { get; set; }
    }
}
