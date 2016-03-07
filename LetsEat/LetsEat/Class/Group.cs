using LetsEat.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat
{
    class Group
    {
        public string _id { get; set; }
        public string name { get; set; }
        public string admin { get; set; }
        public int __v { get; set; }
        public List<Member> members { get; set; }
    }
}
