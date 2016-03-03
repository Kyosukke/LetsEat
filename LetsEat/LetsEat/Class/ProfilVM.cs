using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class ProfilVM
    {
        public string email { get; set; }
    }

    class ProfilRP
    {
        public bool success { get; set; }
        public User user { get; set; }
    }
}
