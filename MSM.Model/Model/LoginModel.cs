using System;
using System.Collections.Generic;
using System.Text;

namespace MSM.Model.Model
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ValidateCode { get; set; }
        public string Token { get; set; }
    }
}
