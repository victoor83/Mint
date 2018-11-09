using System;
using System.Collections.Generic;
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




    }
}
