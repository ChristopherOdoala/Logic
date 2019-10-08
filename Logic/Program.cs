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
            //Console.WriteLine(Check.Marketer.ToString());
            //Console.ReadLine();

            var newString = "rc645832";
            //var newString2 = "BN6453822";
            //var newString3 = "TR24263489";

            if (!newString.StartsWith("RC") && !newString.StartsWith("BN"))
                Console.WriteLine("Invalid");
            else
                Console.WriteLine("Its Correct");
            Console.ReadLine();
        }
    }
}
