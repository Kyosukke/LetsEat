﻿using System;
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
        public string _id { get; set; }
        public string groupeID { get; set; }
        public int __v { get; set; }
        public History history { get; set; }
    }
}
