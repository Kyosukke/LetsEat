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
    }

    class UserRP
    {
        public int id { get; set; }
    }
}
