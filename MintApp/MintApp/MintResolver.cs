using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MintApp
{
    public class MintResolver
    {
        private string _mintPath;
        private string[] _mintArray;
        private List<VisitedPosition> _visitedPositions = new List<VisitedPosition>();
        private Direction _lastDirection;
        private Direction _currentDirection;
        private int _moveCounter = 0;
        public MintResolver(string path)
        {
            _mintPath = path;
            ReadMint();
        }

        public void Resolve()
        {
            Position currentPosition = GetStartPoint();

            _currentDirection = Direction.Right;


            while (true)
            {

                bool wasMovePossible = Move(ref currentPosition);

                if(currentPosition == GetFinishPoint())
                {
                    Console.WriteLine("YEAHH!!");
                    return;
                }

                if(!wasMovePossible)
                {
                    ChangeDirection(currentPosition);
                } else
                {
                    _moveCounter++;
                    //Console.Write($"{ _moveCounter}.");
                }

                if (_moveCounter == 100)
                {

                }

                //temporary to test
                if (_moveCounter > 200)
                    return;
            }
        }

        public void Print()
        {
            int lineCounter = 0;
            Console.WriteLine();
            foreach (var line  in _mintArray)
            {
                //Print with line number
                string lineNr = " ";
                if (lineCounter.ToString().Length == 1)
                    lineNr = lineNr + lineCounter;
                else
                    lineNr = lineCounter.ToString();

                Console.WriteLine(lineNr + line);
                lineCounter++;
            }

            Console.WriteLine("Maze Lenght: " + _mintArray[0].Length);

            Console.WriteLine();
            Console.WriteLine();
        }

        private void ChangeDirection(Position currentPosition)
        {
            switch (_currentDirection)
            {
                case Direction.Right:
                    //Try to go up
                    if(IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        _currentDirection = Direction.Up;         
                    }
                    //Try to go down
                    else if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        _currentDirection = Direction.Down;
                    }
                    //Try to go left
                    else if (IsLocationEmpty(GetNextPosition(Direction.Left, currentPosition)))
                    {
                        _currentDirection = Direction.Left;     
                    }                
                    else
                    {
                        throw new Exception("No move possible");
                    }

                    break;
                case Direction.Left:
                    //Try to go down
                    if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        _currentDirection = Direction.Down;
                    }
                    //Try to go up
                    else if (IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        _currentDirection = Direction.Up;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        _currentDirection = Direction.Right;        
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
                        _currentDirection = Direction.Left;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        _currentDirection = Direction.Right;
                    }
                    //Try to go down
                    else if (IsLocationEmpty(GetNextPosition(Direction.Down, currentPosition)))
                    {
                        _currentDirection = Direction.Down;
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
                        _currentDirection = Direction.Left;
                    }
                    //Try to go right
                    else if (IsLocationEmpty(GetNextPosition(Direction.Right, currentPosition)))
                    {
                        _currentDirection = Direction.Right;
                    }
                    //Try to go up
                    else if (IsLocationEmpty(GetNextPosition(Direction.Up, currentPosition)))
                    {
                        _currentDirection = Direction.Up;
                    }
                    else
                    {
                        throw new Exception("No move possible");
                    }


                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(_currentDirection), _currentDirection, null);
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

        private bool Move(ref Position currentPosition)
        {
            string directionSymbol;
            Position newPosition;
            switch (_currentDirection)
            {
                case Direction.Left:
                    
                    if(PositionAlreadyVisited(currentPosition) &&
                        _lastDirection != Direction.Down)
                    {        
                        var positionUp =GetNextPosition(Direction.Up, currentPosition);
                        if(IsLocationEmpty(positionUp))
                        {
                            if(PositionAlreadyVisited(GetNextPosition(Direction.Left, currentPosition)))
                            {
                                currentPosition = positionUp;
                                directionSymbol = "^";
                                Console.Write(directionSymbol);
                                _lastDirection = Direction.Left;
                                _currentDirection = Direction.Up;
                                _visitedPositions.Add(new VisitedPosition(currentPosition, _currentDirection));
                                return true;
                            }

                        }
                    } 
                    
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
                    throw new ArgumentOutOfRangeException(nameof(_currentDirection), _currentDirection, null);
            }

            if (!IsLocationEmpty(newPosition))            
                return false;


            currentPosition = newPosition;
            _visitedPositions.Add(new VisitedPosition(currentPosition, _currentDirection));
            _lastDirection = _currentDirection;

            Console.Write(directionSymbol);

            return true;
        }


        private bool PositionAlreadyVisited(Position position)
        {
            if(_visitedPositions.Any(x => x.GetPosition() == position))
                return true;

            return false;
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
