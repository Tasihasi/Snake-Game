using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Snake Test = new Snake();
            Test.StartGame();

            Console.WriteLine("press A keyword");
             APressTest();

            Console.WriteLine( Test.AsciiConverter(200));
            Console.WriteLine((char)254);

            Console.ReadLine();
            Console.ReadLine();
        }
        public static void APressTest()
        {
            ConsoleKey Key = Console.ReadKey(true).Key;
            if (Key == ConsoleKey.A)
            {
                Console.WriteLine("pressed A");
            }
        }
        

    }
}
