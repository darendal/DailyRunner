using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DailyRunner
{
    public class Program
    {
        public static DailyChallenge solution = new RackManager1();

        public static void Main(string[] args)
        {
            solution.Start();
            Console.ReadLine();
        }
    }
}
