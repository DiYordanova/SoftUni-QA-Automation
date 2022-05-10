using System;

namespace Summator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Summator summator = new Summator();

            double sum = summator.Sum(new double[] { 1, 2, 3, 4, 5, 6 });

            if (sum == 21)
            {
                Console.WriteLine("Test Pass");
            }

            else
            {
                Console.WriteLine("Test Fail");
            }

            double average = summator.Average(new long[] { 5, 6, 7 });

            if (average == 6)
            {
                Console.WriteLine("Test Pass");
            }

            else
            {
                Console.WriteLine("Test Fail");
            }

        }
    }
}
