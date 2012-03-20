using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bankingexercise
{
    public class Bankingexercise
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo inputVal = Console.ReadKey(true);
            Console.WriteLine(string.Format("You entered {0}", inputVal.Key.ToString()));
            Console.ReadKey();
        }
    }
}
