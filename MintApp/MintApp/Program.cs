using System;
using System.IO;
using System.Net.Mime;

namespace MintApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\maze.txt"));

            MintResolver resolver = new MintResolver(path);
            resolver.Print();
            resolver.Resolve(new ConsoleOutput());

            Console.ReadLine();
        }
    }
}
