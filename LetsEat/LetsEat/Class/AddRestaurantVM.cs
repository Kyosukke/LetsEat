using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class AddRestaurantVM
    {
        public string groupeID { get; set; }
        public string restaurantName { get; set; }
        public string date { get; set; }
    }

    class AddRestaurantRP
    {
        public bool success { get; set; }
        public string message { get; set; }
    }
}
