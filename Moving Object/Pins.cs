using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Object
{
    class Pins : Objects
    {
        private string _name;

        public Pins(int x, int y, string name) : base( x,  y, "Pins")
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
