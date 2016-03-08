using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class RestaurantChoiceVM
    {
        public string groupeID { get; set; }
        public string email { get; set; }
        public string restaurantName { get; set; }
        public string restaurantAdresse { get; set; }
        public string restaurantNumber { get; set; }
    }

    class RestaurantChoiceRP
    {
        public bool success { get; set; }
        //public string message { get; set; }
    }
}
