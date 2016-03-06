using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class GroupVM
    {
        public string email { get; set; }
    }

    class GroupRP
    {
        public bool success { get; set; }
        public List<Group> groups { get; set; }
    }
}
