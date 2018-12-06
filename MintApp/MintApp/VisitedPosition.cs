using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintApp
{
    class VisitedPosition
    {
        private Position _position;
        private Direction _direction;
        public VisitedPosition(Position position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        public Position GetPosition()
        {
            return _position;
        }
    }
}
