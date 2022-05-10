using System.Numerics;

namespace Summator
{
    public class Summator
    {
        public double Sum(double[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }

            return sum;
        }

        public long SubstractionOneNumber(long minuend, long subtrahend)
        {
            minuend -= subtrahend;

            return minuend;
        }

        public long SubstractionMoreThenOneNumber(long minuend, long[] subtrahend)
        {
            for (int i = 0; i < subtrahend.Length; i++)
            {
                minuend -= subtrahend[i];
            }

            return minuend;
        }

        public double Average(long[] arr)
        {
            double sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                sum += arr[i];
            }
            return sum / arr.Length;
        }

        public double Multiply(double num1, double num2)
        {
            double multiply = num1 * num2;

            return multiply;
        }

        public double Division(double num1, double num2)
        {
            double division = num1 / num2;

            return division;
        }
    }
}
