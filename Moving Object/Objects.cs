using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Object
{
    class Objects
    {
        private int _x;
        private int _y;
        private string _type;
        private int _width = 50;
        private int _heigth = 50;
        private int _distance = 49;

        public Objects(int x, int y, string type)
        {
            _x = x;
            _y = y;
            _type = type;
        }

        public int PosX
        {
            get { return _x; }
        }
        public int PosY
        {
            get { return _y; }
        }

        public int Width
        {
            get { return _width; }
        }

        public int Heigth
        {
            get { return _heigth; }
        }

        public string Type
        {
            get { return _type; }
        }

        public void Right()
        {
            _x += _distance;
        }

        public void Left()
        {
            _x -= _distance;
        }

        public void Up()
        {
            _y -= _distance;
        }

        public void Down()
        {
            _y += _distance;
        }

    }
}
