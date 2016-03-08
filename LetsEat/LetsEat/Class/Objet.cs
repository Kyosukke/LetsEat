using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsEat.Class
{
    class Objet
    {
        public string _id { get; set; }
        public string groupeID { get; set; }
        public bool canRandom { get; set; }
        public int __v { get; set; }
        public List<Answer> answers { get; set; }
        public List<Member> members { get; set; }
    }
}
