using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Object
{
    class Bases : Objects
    {
        private string _name;

        public Bases(int x, int y, string name) : base(x,y,"base")
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

    }
}
