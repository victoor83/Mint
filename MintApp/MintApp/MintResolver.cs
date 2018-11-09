using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintApp
{
    public class MintResolver
    {
        private string _mintPath;
        private string[] _mintArray;
        public MintResolver(string path)
        {
            _mintPath = path;
            ReadMint();
        }

        public void Resolve()
        {
            Position startPosition = GetStartPoint();

            bool isEmptyStart = IsLocationEmpty(startPosition);

            while (true)
            {
                if(!Move(Direction.Right, ref startPosition))
                    break;
            }
        }

        public void Print()
        {
            foreach (var line  in _mintArray)
            {
                Console.WriteLine(line);
            }
        }

        private void ReadMint()
        {
            _mintArray = File.ReadAllLines(_mintPath);

        }

        /// <summary>
        /// left down edge
        /// </summary>
        private Position GetStartPoint()
        {
            int width = 1;
            int height = _mintArray.Length - 2;

            return new Position(width, height);
        }

        private bool IsLocationEmpty(Position position)
        {
            if(_mintArray[position.Height][position.Width] == ' ')
            {
                return true;
            }

            return false;
        }

        private bool Move(Direction direction, ref Position currentPosition)
        {
            string directionSymbol;
            Position newPosition;
            switch (direction)
            {
                case Direction.Left:
                    newPosition = new Position(currentPosition.Width - 1, currentPosition.Height);
                    directionSymbol = "<";
                    break;
                case Direction.Right:
                    newPosition = new Position(currentPosition.Width + 1, currentPosition.Height);
                    directionSymbol = ">";
                    break;
                case Direction.Up:
                    newPosition = new Position(currentPosition.Width, currentPosition.Height - 1);
                    directionSymbol = "^";
                    break;
                case Direction.Down:
                    newPosition = new Position(currentPosition.Width, currentPosition.Height + 1);
                    directionSymbol = "v";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            if (!IsLocationEmpty(newPosition))            
                return false;


            currentPosition = newPosition;

            Console.Write(directionSymbol);

            return true;
        }




    }
}
