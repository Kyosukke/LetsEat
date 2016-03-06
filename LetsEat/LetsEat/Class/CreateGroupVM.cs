using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class CreateGroupVM
    {
        public string email { get; set; }
        public string name { get; set; }
    }

    class CreateGroupRP
    {
        public bool success { get; set; }
    }
}
