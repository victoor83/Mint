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
            Position currentPosition = GetStartPoint();

            bool isEmptyStart = IsLocationEmpty(currentPosition);
            Direction currentDirection = Direction.Right;

            int moveCounter = 0;

            while (true)
            {

                bool wasMovePossible = Move(currentDirection, ref currentPosition);

                if(currentPosition == GetFinishPoint())
                    return;

                if(!wasMovePossible)
                {
                    ChangeDirection(ref currentDirection, currentPosition);
                    moveCounter++;
                }

                //temporary to test
                if(moveCounter > 20)
                    return;
            }
        }

        public void Print()
        {
            foreach (var line  in _mintArray)
            {
                Console.WriteLine(line);
            }
        }

        private void ChangeDirection(ref Direction currentDirection, Position currentPosition)
        {
            switch (currentDirection)
            {
                case Direction.Right:
                    //Try to go up
                    if(IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        currentDirection = Direction.Up;         
                    }
                    //Try to go left
                    else if (IsLocationEmpty(GetNextPosition(Direction.Left, currentPosition)))
                    {
                        currentDirection = Direction.Left;     
                    }
                    //Try to go down
                    else if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        currentDirection = Direction.Down;
                    } else
                    {
                        throw new Exception("No move possible");
                    }

                    break;
                case Direction.Left:
                    //Try to go down
                    if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        currentDirection = Direction.Down;
                    }
                    //Try to go up
                    else if (IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        currentDirection = Direction.Up;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        currentDirection = Direction.Right;        
                    }
                    else
                    {
                        throw new Exception("No move possible");
                    }

                    break;
                case Direction.Up:
                    //Try to go left
                    if (IsLocationEmpty(GetNextPosition(Direction.Left, currentPosition)))
                    {
                        currentDirection = Direction.Left;
                    }
                    //Try to go down
                    else if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        currentDirection = Direction.Down;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        currentDirection = Direction.Right;
                    }
                    else
                    {
                        throw new Exception("No move possible");
                    }
                    break;
                case Direction.Down:

                    //Try to go left
                    if (IsLocationEmpty(GetNextPosition(Direction.Left, currentPosition)))
                    {
                        currentDirection = Direction.Left;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        currentDirection = Direction.Right;
                    }
                    //Try to go up
                    else if (IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        currentDirection = Direction.Up;
                    }
                    else
                    {
                        throw new Exception("No move possible");
                    }


                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentDirection), currentDirection, null);
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

        private Position GetFinishPoint()
        {
           return new Position(_mintArray[0].Length - 2, 1);
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
                    newPosition = GetNextPosition(Direction.Left, currentPosition);
                    directionSymbol = "<";
                    break;
                case Direction.Right:
                    newPosition  = GetNextPosition(Direction.Right, currentPosition);
                    directionSymbol = ">";
                    break;
                case Direction.Up:
                    newPosition = GetNextPosition(Direction.Up, currentPosition);
                    directionSymbol = "^";
                    break;
                case Direction.Down:
                    newPosition = GetNextPosition(Direction.Down, currentPosition);
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


        private Position GetNextPosition(Direction direction, Position currentPosition)
        {
            switch (direction)
            {
                case Direction.Left:
                    return new Position(currentPosition.Width - 1, currentPosition.Height);                    
                case Direction.Right:
                    return new Position(currentPosition.Width + 1, currentPosition.Height);  
                case Direction.Up:
                    return new Position(currentPosition.Width, currentPosition.Height - 1);
                case Direction.Down:
                    return new Position(currentPosition.Width, currentPosition.Height + 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

    }
}
