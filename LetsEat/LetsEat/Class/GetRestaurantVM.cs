using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class GetRestaurantVM
    {
        public string groupeID { get; set; }
    }

    class GetRestaurantRP
    {
        public bool success { get; set; }
        public List<History> history { get; set; }
    }
}
