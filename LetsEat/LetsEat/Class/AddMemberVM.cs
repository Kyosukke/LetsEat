using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class AddMemberVM
    {
        public string email { get; set; }
        public string groupeID { get; set; }
    }

    class AddMemberRP
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
