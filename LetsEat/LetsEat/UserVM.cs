using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class UserVM
    {
        public string email { get; set; }
        public string password { get; set; }
        public string pseudo { get; set; }

        public UserVM(string email, string password, string pseudo)
        {
            this.email = email;
            this.password = password;
            this.pseudo = pseudo;
        }
    }

    class UserRP
    {
        public bool success { get; set; }
    }
}
