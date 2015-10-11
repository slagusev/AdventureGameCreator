using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Player
{
    static class RandNums
    {
        static Random r;
        public static long GetRandomLong()
        {
            byte[] buffer = new byte[8];
            r.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static long GetRandomLong(long min, long max)
        {
            EnsureMinLEQMax(ref min, ref max);
            long numbersInRange = unchecked(max - min + 1);
            if (numbersInRange < 0)
                throw new ArgumentException("Size of range between min and max must be less than or equal to Int64.MaxValue");

            long randomOffset = GetRandomLong();
            if (IsModuloBiased(randomOffset, numbersInRange))
                return GetRandomLong(min, max); // Try again
            else
                return min + PositiveModuloOrZero(randomOffset, numbersInRange);
        }

        static bool IsModuloBiased(long randomOffset, long numbersInRange)
        {
            long greatestCompleteRange = numbersInRange * (long.MaxValue / numbersInRange);
            return randomOffset > greatestCompleteRange;
        }

        static long PositiveModuloOrZero(long dividend, long divisor)
        {
            long mod;
            Math.DivRem(dividend, divisor, out mod);
            if (mod < 0)
                mod += divisor;
            return mod;
        }

        static void EnsureMinLEQMax(ref long min, ref long max)
        {
            if (min <= max)
                return;
            long temp = min;
            min = max;
            max = temp;
        }

        static RandNums()
        {
            r = new Random();
        }
        public static int GetNextRandomInt(int max)
        {
            return r.Next(max+1);
        }
        public static TimeSpan GetRandomTimespan(long ticks)
        {
            return TimeSpan.FromTicks(GetRandomLong(0,ticks+1));
        }
        public static TimeSpan GetRandomTimespan(TimeSpan ts)
        {
            return GetRandomTimespan(ts.Ticks);
        }
    }
}
