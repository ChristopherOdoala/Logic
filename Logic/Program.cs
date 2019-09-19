using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    class Program
    {
        public enum Check
        {
            Truck = 1,
            Marketer
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Check.Marketer.ToString());
            Console.ReadLine();
        }
    }
}
