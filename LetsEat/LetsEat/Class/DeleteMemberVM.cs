using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class DeleteMemberVM
    {
        public string groupeID { get; set; }
        public string email { get; set; }
    }

    class DeleteMemberRP
    {
        public bool success { get; set; }
    }
}
