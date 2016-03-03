using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class EditProfilVM
    {
        public string userID { get; set; }
        public string email { get; set; }
        public string pseudo { get; set; }
        public string password { get; set; }
    }

    class EditProfilRP
    {
        public bool success { get; set; }
        public User user { get; set; }
    }
}
