using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class CanRandomVM
    {
        public string groupeID { get; set; }
        public int numberMembre { get; set; }
    }

    class CanRandomRP
    {
        public bool success { get; set; }
        public Objet objet { get; set; }
    }
}
