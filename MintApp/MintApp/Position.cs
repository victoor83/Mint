using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintApp
{
    struct Position
    {
        public int Width{ get; set; }
        public int Height { get; set; }

        public Position(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public static bool operator== (Position a, Position b)
        {
            if(a.Height == b.Height && a.Width == b.Width)
                return true;
            return false;
        }

        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }
    }
}
