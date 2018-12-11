using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MintApp
{
    class VisitedPosition
    {
        private Position _position;
        private Direction _direction;
        private bool _visitedWithDirectionChange;

        public int chuj = 2;

        //true if position has been alredy visited, but the direction is changed to not visited position(location)
        public bool VisitedWithDirectionChange
        {
            get
            {
                return _visitedWithDirectionChange;
            }
        }

        /// <summary>
        /// data of visited position in maze
        /// </summary>
        /// <param name="position">position in maze</param>
        /// <param name="direction">came from which direction</param>
        public VisitedPosition(Position position, Direction direction)
        {
            _position = position;
            _direction = direction;
        }

        public void SetDirectionChangedFlag()
        {
            _visitedWithDirectionChange = true;
        }

        public Position GetPosition()
        {
            return _position;
        }

        public Direction GetDirection()
        {
            return _direction;
        }
    }
}
